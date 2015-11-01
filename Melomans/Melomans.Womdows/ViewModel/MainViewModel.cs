using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Melomans.Core.Message;
using Melomans.Core.Models;
using Melomans.Core.Network;
using Melomans.Windows.Network;
using Sockets.Plugin;

namespace Melomans.Windows.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INetworkMessageRouter _router;
        private readonly NetworkSettings _settings;
        private ObservableCollection<CommsInterface> _commsInterfaces;
        private bool _isRunning;
        private CommsInterface _selectedInterface;
        private IMessageReceiverConfig<HelloMessage> _helloMessageToken;
        private IMessageReceiverConfig<EchoMessage> _echoMessageToken;

        public MainViewModel(INetworkMessageRouter router, NetworkSettings settings)
        {
            _router = router;
            _settings = settings;
            StartCommand = new RelayCommand(OnStart, () => SelectedInterface != null);
            RefreshCommand = new RelayCommand(OnRefresh, () => IsRunning);
            Melomans = new ObservableCollection<Meloman>();
            _helloMessageToken = _router.Subscribe<HelloMessage>()
                .OnSuccess(m =>
                {
                    Melomans.Add(m.Meloman);
                   /* _router.PublishFor(new [] {m.Meloman}, new EchoMessage
                    {
                        Meloman = new Meloman
                        {
                            Id = _settings.Adaptes.NativeInterfaceId,
                            IpAddress = _settings.Adaptes.IpAddress,
                            Port = _settings.ListenPort,
                            Title = Environment.MachineName
                        }
                    }).First().Run();*/
                });
            _echoMessageToken = _router.Subscribe<EchoMessage>()
                .OnSuccess(m =>
                {
                    Melomans.Add(m.Meloman);
                });
            Initialize();
        }

        private void OnRefresh()
        {
            _router.Publish(new HelloMessage
            {
                Meloman = new Meloman
                {
                    Id = _settings.Adaptes.NativeInterfaceId,
                    IpAddress = _settings.Adaptes.IpAddress,
                    Port = _settings.ListenPort,
                    Title = Environment.MachineName
                }
            }).Run();
        }

        private void OnStart()
        {
            if (!IsRunning)
            {
                _settings.Adaptes = SelectedInterface;
                _settings.TTL = 1;
                _settings.MulticastAddress = "224.0.0.1";
                IsRunning = true;
                _router.Start();
            }
            else
            {
                _router.Stop();
                IsRunning = false;
            }

        }

        async void Initialize()
        {
           CommsInterfaces = new ObservableCollection<CommsInterface>(await CommsInterface.GetAllInterfacesAsync().ConfigureAwait(true)); 
        }

        public CommsInterface SelectedInterface
        {
            get { return _selectedInterface; }
            set
            {
                if (!Equals(_selectedInterface, value))
                {
                    _selectedInterface = value;
                    RaisePropertyChanged(() => SelectedInterface);
                }
            }
        }

        public ObservableCollection<CommsInterface> CommsInterfaces
        {
            get { return _commsInterfaces; }
            set
            {
                if (!Equals(_commsInterfaces, value))
                {
                    _commsInterfaces = value;
                    RaisePropertyChanged(() => CommsInterfaces);
                }
            }
        }

        public ObservableCollection<Meloman> Melomans { get; private set; }

        public ICommand StartCommand { get; private set; }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (!Equals(_isRunning, value))
                {
                    _isRunning = value;
                    RaisePropertyChanged(() => IsRunning);
                }
            }
        }

        public ICommand RefreshCommand { get; private set; }
    }
}