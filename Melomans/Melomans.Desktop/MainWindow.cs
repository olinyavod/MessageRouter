using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Gtk;
using Melomans.Core.Message;
using Melomans.Core.Models;
using Melomans.Core.Network;
using Melomans.Desktop.Network;
using Sockets.Plugin;
using IContainer = Autofac.IContainer;

public partial class MainWindow: Gtk.Window
{
    private readonly IContainer _container;
    private readonly INetworkMessageRouter _router;
    private IMessageReceiverConfig<EchoMessage> _echoToken;
    private ListStore _adaptersStore;
    private IEnumerable<CommsInterface> _commsInterfaces;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
        var builder = new ContainerBuilder();
	    builder.RegisterModule<NetworkModule>();
	    _container = builder.Build();
	    _router = _container.Resolve<INetworkMessageRouter>();
	    _echoToken = _router.Subscribe<EchoMessage>().OnSuccess(m =>
	    {
	        
	    });
	    Build ();
        btnStart.Clicked += BtnStartOnButtonPressEvent;
        _adaptersStore = new ListStore(typeof(string));
        cmbAdapters.Model = _adaptersStore;
        btnRefresh.Clicked += BtnRefreshOnButtonPressEvent;
        this.Shown += OnShown;
        Initialize();
        
	}

    private void OnShown(object sender, EventArgs eventArgs)
    {
        Initialize();
    }

    private void BtnRefreshOnButtonPressEvent(object o, EventArgs args)
    {
        var selected = _commsInterfaces.FirstOrDefault(i => i.Name == cmbAdapters.ActiveText);
        _router.Publish(new HelloMessage
        {
            Meloman = new Meloman
            {
                Id = Environment.MachineName,
                IpAddress = selected.IpAddress,
                Port = _container.Resolve<NetworkSettings>().ListenPort,
                Title = Environment.MachineName
            }
			}).Run();
    }

    private void BtnStartOnButtonPressEvent(object o, EventArgs args)
    {
        var networkSettings = _container.Resolve<NetworkSettings>();
        networkSettings.Adaptes = _commsInterfaces.FirstOrDefault(i => i.Name == cmbAdapters.ActiveText);
        networkSettings.MulticastAddress = "224.0.0.0";
        networkSettings.MulticastPort = 30303;
        networkSettings.ListenPort = 30303;
        networkSettings.TTL = 1;
        btnStart.Label = "Stop";
        _router.Start();
    }

    async void Initialize()
    {
        _commsInterfaces = await CommsInterface.GetAllInterfacesAsync()
            .ConfigureAwait(false);

        
       
       foreach (var i in _commsInterfaces)
            _adaptersStore.AppendValues(i.Name);
    }

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
