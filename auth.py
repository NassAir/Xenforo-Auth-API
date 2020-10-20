
//pas fini * not finish
import requests
import json

payload = {'login': user, 'password': passs}
r = requests.post('https://example.com(/index.php)/api/auth', data = payload)

y = json.loads(r)

print(r_dict['user'])
