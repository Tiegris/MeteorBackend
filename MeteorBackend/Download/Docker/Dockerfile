FROM python:3.9.1-slim
WORKDIR /app
EXPOSE 80

COPY ["Download/src/requirements.txt", "."]
RUN pip install --trusted-host pypi.python.org -r requirements.txt

COPY ["Download/src/app.py", "."]
CMD ["python", "app.py"]