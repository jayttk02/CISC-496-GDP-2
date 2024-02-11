from time import sleep
import requests

for x in range(10000):
    x = requests.get('https://beccadvn.pythonanywhere.com/')
    print(x.status_code)