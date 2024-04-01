using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext
    //representa o Banco de dados
{
    public DbSet<Blog> Blogs { get; set; }
    //cada classe DbSet vira uma tabela com determinadas colunas
    //correspondetes aos itens da classe( Nesse caso,a classe referida é o Blog)
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
    //essa linha informa o relacionamento entre o Blog e o Post,pois existe uma lista de posts
}

public class Post
{
    public int PostId { get; set; }
    //A utilizacao da convencao NOMECLASSE + ID ,no caso, representada por 
    //PostID, mostra ao entity que ela é primary key da classe
    //Com isso, observa-se que ele auto incrementa de acordo com a adicao ao banco
    //alem de forcar que seja unica e etc
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    //Como todo Post advem de um blog, observa-se sua utilizacao para 
    //melhor navegacao, pois ese esta linkado ao blog
}