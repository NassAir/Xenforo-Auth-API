#include <iostream>
#include <fstream>
#include <direct.h>
#include <cstdlib>
#include <string>
#include <string.h>
#include <fstream.h>
#include <iostream.h>
#include "request/request.hpp"
#include "rapidjson/document.h"
#include "rapidjson/pointer.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/writer.h"
#include "utils/utils.hpp"
using namespace std;
using namespace http;
using namespace rapidjson;

int main()
{

    string username, password, key;
    bool success; 

    ifstream fichier("key.txt");
    getline(key, ligne);
    
    int n = 1; // pour récupérer la key dans fichier 1ere ligne * to retrieve the key in file 1st line
  int i = 0;

  // Lire fichier key.txt * Read key.txt file
  std::ifstream fichier("key.txt");

  if( fichier )// Si le fichier n'est pas ouvert, la conditione est fausse * If the file is not open, the condition is false
  {
  std::string ligne; // Variable pour chaque ligne ( si plusieurs) * Variable for each row (if several)

  while( std::getline( fichier, ligne )) //  La condiition s'arrête si erreur * The condition stops if error
  {
  if(i == n )
  {
  // Affiche la key à l'écran * Display the key on the screen
   std::cout << ligne << std::endl;
  }
  i++;
  }
    
    
    
    cout << "Your username ?" << endl; // Demande le pseudo ( focntionne aussi avec le mail) * Ask for the nickname (also works with the email)
    cin >> username; // enregistre le pseudo ou mail * register the username or email
    cout << "Your password ?" << endl; // Demande le mot de passe * Request the password
    cin >> password; // enregistre le mot de passe * save password

    try 
    {
        Request request("http://example.com(/index.php)/api/auth"); // Entrez votre domaine, vérifiez si vous avez activez les URL conviviales ( si ya index.php) * Enter your domain, check if you have enabled friendly urls (if there is index.php)
        
        const Response postResponse = request.send("POST", "login=" + username + "&password=" + password, { // requête POST
        "Content-Type: application/x-www-form-urlencoded", "XF-Api-Key: " + key // Vous pouvez mettre votre key directement et supprimer les lignes précédantes * You can put your key directly and delete the previous lines
        });

        // Pour capturer les informations en JSON * To capture information in JSON
        Document document;
        document.Parse(string(postResponse.body.begin(), postResponse.body.end()).c_str());
      
        assert(document["success"].IsBool()); // Comme on le voit facilement, vérifie si "success" est boolén (vrai ou faux) * As you can easily see, check if "success" is boolén (true or false)
        success = document["success"].GetBool(); // Capture le json de success * Capture the success json

        if (success == true)
        {
            cout << "Authentication Success" << endl;

          // La partie qui va suivre n'a pas beaucoup d'utilité vous pouvez la supprimer, il va juste vérifier si la personne fait partie du staff * The following part is not very useful you can delete it, it will just check if the person is part of the staff
            if (Value* user = GetValueByPointer(document, "/user")) 
            {
                if ((*user)["is_staff"].GetBool() == true) { 
                    cout << "Hi Boss !" << endl;
                }
                else 
                {
                    cout << "Hi member !" << endl;
                    return 0;

                        
                    }
                }
            }
        }
        else
        {
            cout << "Authentication error, please check" << endl; // Pseudo ou mot de passe invalide * Invalid username or password
        }

        system("pause");
    }
    catch (const exception & e) // Si il ya une erreur * If there is an error
    {
        cerr << "Error in request, show error : " << e.what() << endl; // Affiche les erreurs * Show errors

        system("pause");
        return EXIT_FAILURE; //Sors du programme avec une erreur, dont le statut code vaut 1 * Exit the program with an error, whose code status is 1
    }

    return EXIT_SUCCESS; //Sors du programme avec succès, dont le statut code vaut 0 * Exit the program successfully, whose code status is 0
}
