namespace Core.Persistence.Paging;

public class BasePageableModel
{
    //kaçıncı sayfadayım
    public int Index { get; set; }
    //bir sayfa da kaçtane data var
    public int Size { get; set; }
    //toplamda kaç tane data var
    public int Count { get; set; }
    //toplamda kaç tane page var
    public int Pages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
}