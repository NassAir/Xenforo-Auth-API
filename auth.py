
#pas fini * not finish
import requests
import json

def xenauth():
  
  if os.path.isfile('key.txt'):
    key = str(input('Enter Key: '))
    username = str(input('Enter Pseudo/Mail : '))
    password = str(input('Enter Password : '))
    
  else:
    k = open('key.txt', 'r')
    key = k.read()
    username = str(input('Enter Pseudo/Mail : '))
    password = str(input('Enter Password : '))
  
payload = {'login': user, 'password': passs}
r = requests.post('https://example.com(/index.php)/api/auth', data = payload)

y = json.loads(r)

print(r_dict['user'])
