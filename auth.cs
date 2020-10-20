using System;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Text;

// Juste pour ne pas faire planter le programme avec le statut code * Just so as not to crash the program with the status code
public static class WebRequestExtensions
{
	public static WebResponse GetResponseWithoutException(this WebRequest request)
	{
		if (request == null)
		{
			throw new ArgumentNullException("request");
		}

		try
		{
			return request.GetResponse();
		}
		catch (WebException e)
		{
			if (e.Response == null)
			{
				throw;
			}

			return e.Response;
		}
	}
}

namespace Auth
{
	public class Ayeh
	{
		public static void Auth()
		{
			Console.Title = "Auth";
			//clé * key
			string t3;
      
			if (File.Exists("key.txt"))
			{
				t3 = File.ReadAllText("key.txt");
			}
			else
			{
				Console.WriteLine("Enter the key :");
				t3 = Console.ReadLine();
				File.WriteAllText("key.txt", t3);
			}

			//pseudo
			string pseudo = "";
			if (File.Exists("pseudo.txt"))
			{
				pseudo = File.ReadAllText("pseudo.txt");
			}
			else
			{
				Console.Write("Enter your pseudo or mail ");
				pseudo = Console.ReadLine();
			}
			//mdp * password
			Console.Write("Enter your password");
			string password = Console.ReadLine();
			Console.Clear(); // Efface sinon c'est un peu moche * Erase otherwise it's a little ugly

			//requete
			var request = (HttpWebRequest)WebRequest.Create("https://example.com(/index.php)/api/auth/"); // Remplacez avec votre domaine * Replace with your domain
			var postData = "login=" + Uri.EscapeDataString(pseudo);
			postData += "&password=" + Uri.EscapeDataString(password);
			var data = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;
			request.Headers.Add("XF-Api-Key", t3); // Vous pouvez supprimer les anciennes lignes et mettre la clé directement * You can delete the old lines and put the key directly

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			//statuts code
			var reponse = (HttpWebResponse)request.GetResponseWithoutException();

			if (reponse.StatusCode == HttpStatusCode.BadRequest)//erreur 400 = manque un truc * error  404 = Not found
			{
				Console.WriteLine("Incorrect Pseudo/Mail or Paassword...");
				if (File.Exists("pseudo.txt"))
				{
					File.Delete("pseudo.txt");
				}
				Thread.Sleep(4000);
				Environment.Exit(0);
			}
			if (reponse.StatusCode == HttpStatusCode.Unauthorized)// erreur 401 =  accès non autorisé * error 401 = Unauthorized
			{
				Console.WriteLine("Your key is invalid or missing... ");
				if (File.Exists("pseudo.txt"))
				{
					File.Delete("pseudo.txt");
				}
				Thread.Sleep(4000);
				Environment.Exit(0);
			}
			if (reponse.StatusCode == HttpStatusCode.ServiceUnavailable)//maintenance : erreur 503 =  Service non dispo * error 503 = ServiceUnavailable
			{
				Console.WriteLine("Sorry, Example is maintenance."); // Remplacez par le nom de votre forum * Replace with the name of your forum
				Thread.Sleep(4000);
				Environment.Exit(0);
			}
			if (reponse.StatusCode == HttpStatusCode.OK) // Bon * good
			{
				//re-post parce que bon * re-post because is good 
				var requestn = (HttpWebRequest)WebRequest.Create("https://etrigan.to/api/auth/");
				var postDatan = "login=" + Uri.EscapeDataString(pseudo);
				postDatan += "&password=" + Uri.EscapeDataString(mdp);
				var datan = Encoding.ASCII.GetBytes(postDatan);

				requestn.Method = "POST";
				requestn.ContentType = "application/x-www-form-urlencoded";
				requestn.ContentLength = datan.Length;
				requestn.Headers.Add("XF-Api-Key", t3);

				using (var streamn = requestn.GetRequestStream())
				{
					streamn.Write(datan, 0, datan.Length);
				}
				//recupérer pour capturer en json * recover to capture in json
				var response = (HttpWebResponse)requestn.GetResponse();
				var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
				//json
				var ayeh = JsonConvert.DeserializeObject<dynamic>(responseString);
				var git = ayeh.user;
				Console.WriteLine("Hi {0} !", git.username.ToString());
        
			}
			Console.WriteLine("     ");
		}
	}
}
