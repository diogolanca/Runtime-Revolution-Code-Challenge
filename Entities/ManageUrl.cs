using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CodeChallenge.Entities
{
    public class ManageUrl : IManageUrl
    {
        public Task<Stat> Click(string segment, string referer, string ip)
        {
            return Task.Run(() =>
            {
                using (var ctx = new CodeChallengeContext())
                {
                    ShortUrl url = ctx.ShortUrls.Where(u => u.Segment == segment).FirstOrDefault();
                    if (url == null)
                    {
                        throw new CCNotFoundException();
                    }

                    url.NumOfClicks = url.NumOfClicks + 1;

                    Stat stat = new Stat()
                    {
                        ClickDate = DateTime.Now,
                        Ip = ip,
                        Referer = referer,
                        ShortUrl = url
                    };

                    ctx.Stats.Add(stat);

                    ctx.SaveChanges();

                    return stat;
                }
            });
        }

        public Task<ShortUrl> ShortenUrl(string fullUrl, string ip, string segment = "")
        {
            return Task.Run(() =>
            {
                using (var ctx = new CodeChallengeContext())
                {
                    ShortUrl url;

                    url = ctx.ShortUrls.Where(u => u.FullUrl == fullUrl).FirstOrDefault();
                    if (url != null)
                    {
                        return url;
                    }

                    if (!string.IsNullOrEmpty(segment))
                    {
                        if (ctx.ShortUrls.Where(u => u.Segment == segment).Any())
                        {
                            throw new CC_ConflictException();
                        }
                    }
                    else
                    {
                        segment = this.NewSegment();
                    }

                    if (string.IsNullOrEmpty(segment))
                    {
                        throw new ArgumentException("Segment is empty");
                    }

                    url = new ShortUrl()
                    {
                        Added = DateTime.Now,
                        Ip = ip,
                        FullUrl = fullUrl,
                        NumOfClicks = 0,
                        Segment = segment
                    };

                    ctx.ShortUrls.Add(url);

                    ctx.SaveChanges();

                    return url;
                }
            });
        }

        private string NewSegment()
        {
            using (var ctx = new CodeChallengeContext())
            {
                int i = 0;
                while (true)
                {
                    string segment = Guid.NewGuid().ToString().Substring(0, 6);
                    if (!ctx.ShortUrls.Where(u => u.Segment == segment).Any())
                    {
                        return segment;
                    }
                    if (i > 30)
                    {
                        break;
                    }
                    i++;
                }
                return string.Empty;
            }
        }
    }
}