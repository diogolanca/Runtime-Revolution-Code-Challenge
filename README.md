# CodeChallenge
 
	-O desafio proposto pela Runtime Revolution foi a criação de um URL Shortener na linguagem e framework à escolha.
	-A escolha foi a framework asp.net framework 4.5 em c# devido à minha experiência com a linguagem.
	-A preferência por esta framework sobre o Core 3.1 deveu-se ao facto de após analisar o desafio proposto, conclui que a framework Core necessitaria de alguma pesquisa maior para implementar a solução com base no que encontrei na análise inicial o que ia implicar um tempo maior desnecessário na realização do mesmo.

	-Para executar a solução bastará abrir a mesma com uma versão do Visual Studio, a versão utilizada foi o 2019 mas tanto o Visual Studio 2017 como o Visual Studio Code deverão executar a solução sem qualquer imprevisto.
	-Para evitar que seja necessário reconstruir os ficheiros da base de dados foi alterado o gitignore para que os mesmos fossem carregados para o repositório.

	Execução do desafio: 

	-UrlController:	Controlador responsável pelas funcionalidades da View em Url/Index.cshtml
		ActionResult Index() : Inicializa a classe Url que vai ser carregada na view quando chamada a primeira vez.
		ActionResult Index(Url url): Executa cada vez que o botão de submit é pressionado e valida se o modelo de classe Url é válido.
		ActionResult Click(): Chamada quando o link shorten URL é clicado e executa o redirecionamento para o link através de um novo separador.
	
	-RouteConfig.cs: Adicionado a configuração que permite chamar o ActionResult Click() assim como alterada a configuração inicial para chamar o index do controlador Url em vez do pré-definido Home.
	
	-IManageUrl: Interface que define os métodos necessários.
		Task<ShortUrl> ShortenUrl e Task<Stat> Click

	-Url: Classe usada como modelo para carregar os dados de e para a View

	-ManageUrl: Classe derivada de IManageUrl que vai construir os métodos necessários à execução do desafio proposto.
		Task<Stat> Click: Método que solicita à base de dados o url real com base no segment, ao mesmo tempo que permite contabilizar o numero de clicks no segmento, adicionando tambem à base de dados a data do click, o ip de origem do pedido, e o url onde se encontrava o link.
		Task<ShortUrl> ShortenUrl: verifica se o URL solicitado já existe, caso já exista retorna o já existente. Caso contrário cria uma nova classe ShortUrl que vai ser armazenada na base de dados e carregar os dados de volta para a View. Este método também valida que caso exista já um segmento personalizado correspondente não permita a utilização do mesmo.
		NewSegment(): este método vai criar um segmento aleatório com base na criação de um GUID validando também se já existe esse segment criado na base de dados, caso já exista ele segue num ciclo While até gerar um segment que ainda não exista na base de dados. Tendo em conta a possibilidade de repetições ser pequena, gerei um limite de 30 tentativas de gerar um segment.

	-ShortUrl.cs e Stat.cs: São os modelos de classe que replicam as colunas existentes nas tabelas da base de dados de forma a tornar possivel o carregamento dos dados através do modelo MVC.
		ShorUrl: contém a associação do URL completo com o short URL assim como data de criação e número de cliques. É através desta tabela que se associa para onde executar o redirecionamento do short URL.
		Stat: Permite armazenar estatisticas com base na data que foi clicado um determinado short URL

	-UnityConfig: Permite fazer dependecy injection sem necessidade de utilização de construtores para a interface IManageUrl e a classe ManageUrl que deriva da interface.

	O tratamento de erros é gerido através de 3 classes:
		-CCErrorFilter: esta classe é adicionada em filterConfig.cs e deriva do HandleErrorAttribute para tratar os erros mais previsiveis de ocorrer.
		-CCNotFoundException e CC_ConflictException: classes criadas com o objetivo de poder filtrar as excepções e encaminhar para as páginas correspondentes pelo CCErrorFilter.
		-Foram criadas 3 páginas de erro na pasta Shared para apresentação ao utilizador que são apresentadas com base na identificação dos erros no CCErrorFilter.
			Error500: página apresentada quando não se trata de erro de conflict(nome de segmento já existente) ou Not Found. 
	 		Error404: página apresentada quando erro é Not Found
	 		Error409: Página apresentada quando erro de conflict

	A base de dados é um local mdf file criado através da metodologia code-first.
		
 
		
