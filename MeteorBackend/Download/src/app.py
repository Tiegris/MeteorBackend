from flask import Flask, make_response, jsonify
from flask import request
from flask_json import FlaskJSON, as_json
from pymongo import MongoClient
import os

mongo_url = os.getenv('METEOR_MongoUrl', 'mongodb://mongodb:27017')

mongo = MongoClient(mongo_url)
app = Flask(__name__)
FlaskJSON(app)


@app.route('/')
def hello_world():
    return "I'm alive!"


@app.route("/api/download", methods=['GET'])
@as_json
def get():
    result = []
    collection = mongo.meteor.uploads
    for item in collection.find():
        del item['_id']
        result.append(item)

    return result


if __name__ == '__main__':
    app.run(host="0.0.0.0", port=80, debug=False)
