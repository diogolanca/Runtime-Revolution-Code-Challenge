using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Entities
{
    public interface IManageUrl
    {
        Task<ShortUrl> ShortenUrl(string fullUrl, string ip, string segment = "");
        Task<Stat> Click(string segment, string referer, string ip);
    }
}
