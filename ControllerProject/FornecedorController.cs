using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelProject;
using PersistenceProject;

namespace ControllerProject
{
    public class FornecedorController
    {
        private Repository repository = new Repository();
        
        //insert
        public Fornecedor Insert(Fornecedor fornecedor)
        {
            return this.repository.InsertFornecedor(fornecedor);
        }
    
        //remove
        public void Remove(Fornecedor fornecedor)
        {
            this.repository.RemoveFornecedor(fornecedor);
        }

        //getAll
        public IList<Fornecedor> GetAll()
        {
            return this.repository.GetAllFornecedores();
        }

        //update
        public Fornecedor Update(Fornecedor fornecedor)
        {
            return this.repository.UpdateFornecedor(fornecedor);
        }
    }
}
