using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace HostService
{
    [RunInstaller(true)]
    public class ProjectInstaller:Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "TSDYKJ_PublishService";
            service.Description = "唐山市达意科技快递业务发布服务";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
