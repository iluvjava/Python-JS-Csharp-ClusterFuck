import requests
from bs4 import BeautifulSoup
import shutil
import re

url = "https://www.deviantart.com/rainbow-highway/art/Phencyclidine-8k-736954613"

session = requests.Session()
res = session.get(url)
html = res.text

soup = BeautifulSoup(html, features="html5lib")

dlbtn = soup.find_all(class_="dev-page-download")

print(dlbtn)


if len(dlbtn):
    print(dlbtn)
    first = dlbtn[0]
    href = first['href']
    print(href)
    r = session.get(href, stream=True)
    if r.status_code == 200:
        d = r.headers['content-disposition']
        path = d.split("\'\'")[1]
        with open(path, 'wb') as f:
            r.raw.decode_content = True
            shutil.copyfileobj(r.raw, f)
else:
    print("No download found")