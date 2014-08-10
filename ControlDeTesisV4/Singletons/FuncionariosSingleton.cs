using System;
using System.Collections.ObjectModel;
using System.Linq;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;

namespace ControlDeTesisV4.Singletons
{
    public class FuncionariosSingleton
    {
        private static ObservableCollection<Funcionarios> ponentes;
        private static ObservableCollection<Funcionarios> signatarios;
        private static ObservableCollection<Funcionarios> abogResp;
        private static ObservableCollection<Funcionarios> apoyos;


        private FuncionariosSingleton()
        {
        }

        public static ObservableCollection<Funcionarios> Ponentes
        {
            get
            {
                if (ponentes == null)
                    ponentes = new FuncionariosModel().GetPonentes();

                return ponentes;
            }
        }

        public static ObservableCollection<Funcionarios> Signatarios
        {
            get
            {
                if (signatarios == null)
                    signatarios = new FuncionariosModel().GetSignatarios();

                return signatarios;
            }
        }

        public static ObservableCollection<Funcionarios> AbogResp
        {
            get
            {
                if (abogResp == null)
                    abogResp = new FuncionariosModel().GetAbogados(1);

                return abogResp;
            }
        }

        public static ObservableCollection<Funcionarios> Apoyos
        {
            get
            {
                if (apoyos == null)
                    apoyos = new FuncionariosModel().GetAbogados(2);

                return apoyos;
            }
        }
    }
}
