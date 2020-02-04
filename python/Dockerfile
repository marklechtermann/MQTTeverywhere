FROM python:alpine3.11

WORKDIR /app
ADD . .
RUN pip install --no-cache-dir -r requirements.txt

CMD [ "python", "app.py" ]