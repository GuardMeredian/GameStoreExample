using System;

namespace GameStore.WebUI.Models
{
    public class PagingInfo
    {
        //кол-во товаров
        public int TotalItems { get; set; }

        //кол-во товаров на одной странице
        public int ItemsPerPage { get; set; }

        //Номер текущей страницы
        public int CurrentPage { get; set; }

        //Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}