using Cassandra;
using Cassandra.Mapping;
using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CassandraQuickStart
{
	public class Program
	{
		// Cassandra Cluster Configs      
		private const string USERNAME = "<PROVIDE>";
		private const string PASSWORD = "<PROVIDE>";
		private const string CASSANDRACONTACTPOINT = "<PROVIDE>.cassandra.cosmos.azure.com";  // DnsName  
		private static int CASSANDRAPORT = 10350;

		static void Main(string[] args)
		{
			ProcessAsync().Wait();

			Console.WriteLine("Done! You can go look at your data in Cosmos DB. Don't forget to clean up Azure resources!");
			Console.WriteLine("Press any key to exit.");

			Console.ReadKey();
		}

		public static async Task ProcessAsync()
		{
			// Connect to cassandra cluster  (Cassandra API on Azure Cosmos DB supports only TLSv1.2)
			var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);

			options.SetHostNameResolver((ipAddress) => CASSANDRACONTACTPOINT);
			Cluster cluster = Cluster
				.Builder()
				.WithCredentials(USERNAME, PASSWORD)
				.WithPort(CASSANDRAPORT)
				.AddContactPoint(CASSANDRACONTACTPOINT)
				.WithSSL(options)
				.Build()
			;

			ISession session = await cluster.ConnectAsync();

			// Creating KeySpace and table
			await session.ExecuteAsync(new SimpleStatement("DROP KEYSPACE IF EXISTS uprofile"));
			await session.ExecuteAsync(new SimpleStatement("CREATE KEYSPACE uprofile WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'datacenter1' : 1 };"));
			Console.WriteLine(String.Format("created keyspace uprofile"));
			await session.ExecuteAsync(new SimpleStatement("CREATE TABLE IF NOT EXISTS uprofile.user (user_id int PRIMARY KEY, user_name text, user_bcity text)"));
			Console.WriteLine(String.Format("created table user"));

			session = await cluster.ConnectAsync("uprofile");
			IMapper mapper = new Mapper(session);

			// Inserting Data into user table
			await mapper.InsertAsync<User>(new User(1, "LyubovK", "Dubai"));
			await mapper.InsertAsync<User>(new User(2, "JiriK", "Toronto"));
			await mapper.InsertAsync<User>(new User(3, "IvanH", "Mumbai"));
			await mapper.InsertAsync<User>(new User(4, "LiliyaB", "Seattle"));
			await mapper.InsertAsync<User>(new User(5, "JindrichH", "Buenos Aires"));
			Console.WriteLine("Inserted data into user table");

			Console.WriteLine("Select ALL");
			Console.WriteLine("-------------------------------");
			foreach (User user in await mapper.FetchAsync<User>("Select * from user"))
			{
				Console.WriteLine(user);
			}

			Console.WriteLine("Getting by id 3");
			Console.WriteLine("-------------------------------");
			User userId3 = await mapper.FirstOrDefaultAsync<User>("Select * from user where user_id = ?", 3);
			Console.WriteLine(userId3);

			// Clean up of Table and KeySpace - commented out since other QuickStarts do not immediately delete what was done,
			// and so QuickStart user (that's you) has chance to go look at this data in Cosmos DB
			//session.Execute("DROP table user");
			//session.Execute("DROP KEYSPACE uprofile");
		}

		public static bool ValidateServerCertificate
		(
			object sender,
			X509Certificate certificate,
			X509Chain chain,
			SslPolicyErrors sslPolicyErrors
		)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
				return true;

			Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
			// Do not allow this client to communicate with unauthenticated servers.
			return false;
		}
	}
}
