using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.FileStream;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.BinaryHTTPClient.Notification;
using UpdateControls.Fields;
using UpdateControls.Correspondence.Memory;
using IDidIt.Model;

namespace IDidIt
{
    public class SynchronizationService
    {
        private const string ThisIndividual = "IDidIt.Individual.this";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Independent<Individual> _individual = new Independent<Individual>(
            Individual.GetNullInstance());

        public void Initialize()
        {
            var storage = new FileStreamStorageStrategy();
            var http = new HTTPConfigurationProvider();
            var communication = new BinaryHTTPAsynchronousCommunicationStrategy(http);

            _community = new Community(storage);
            _community.AddAsynchronousCommunicationStrategy(communication);
            _community.Register<CorrespondenceModel>();
            _community.Subscribe(() => Individual);

            CreateIndividual(http);

            // Synchronize whenever the user has something to send.
            _community.FactAdded += delegate
            {
                Synchronize();
            };

            // Synchronize when the network becomes available.
            bool networkAvailable = NetworkInterface.GetIsNetworkAvailable();
            System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += (sender, e) =>
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                    Synchronize();
            };

            // And synchronize on startup or resume.
            if (networkAvailable)
                Synchronize();
        }

        public void InitializeDesignData()
        {
            _community = new Community(new MemoryStorageStrategy());
            _community.Register<CorrespondenceModel>();

            CreateIndividualDesignData();
        }

        public Community Community
        {
            get { return _community; }
        }

        public Individual Individual
        {
            get
            {
                lock (this)
                {
                    return _individual;
                }
            }
            private set
            {
                lock (this)
                {
                    _individual.Value = value;
                }
            }
        }

        public void Synchronize()
        {
            _community.BeginSending();
            _community.BeginReceiving();
        }

        private void CreateIndividual(HTTPConfigurationProvider http)
        {
			_community.Perform(async delegate
			{
				Individual individual = await _community.LoadFactAsync<Individual>(ThisIndividual);
				if (individual == null)
				{
					string randomId = Punctuation.Replace(Guid.NewGuid().ToString(), String.Empty).ToLower();
					individual = await _community.AddFactAsync(new Individual(randomId));
					await _community.SetFactAsync(ThisIndividual, individual);
				}
				Individual = individual;
				http.Individual = individual;
			});
        }

        private void CreateIndividualDesignData()
        {
			_community.Perform(async delegate
			{
				var individual = await _community.AddFactAsync(new Individual("design"));
				Individual = individual;
			});
        }
    }
}
