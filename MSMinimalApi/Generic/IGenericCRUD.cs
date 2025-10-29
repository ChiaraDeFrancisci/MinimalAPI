namespace MyApp.Generic;

public interface IGenericCRUD
{
    //interfaccia generica
    interface IGenericCRUD<T, U, X>
    {
        public Task<List<T>> GetAllItems();
        public Task<T?> GetItem(U Id);
        public Task<T> CreateItem(X newItem);
        public Task UpdateItem(T itemModificato);
        public Task DeleteItem(U Id);
    }
}
