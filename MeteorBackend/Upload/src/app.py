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
    return 'Hello World!'


@app.route("/api/upload", methods=['POST'])
@as_json
def upload():
    try:
        data = request.json
        if validate(data):
            insert(data)
            del data['_id']
            return data
        else:
            return make_response(jsonify({'error': 'Invalid format'}), 400)
    except:
        return make_response(jsonify({'error': 'Exception'}), 500)


@app.route("/api/upload", methods=['GET'])
@as_json
def get():
    result = []
    collection = mongo.meteor.uploads
    for item in collection.find():
        del item['_id']
        result.append(item)

    return result


def validate(data):
    cycles = data.get('Cycles')
    if cycles is None or len(cycles) <= 0:
        return False
    if data.get('Scheme') is None:
        return False
    if data.get('UsedProfile') is None:
        return False
    if data.get('OfficialStart') is None:
        return False
    return True


def insert(data):
    collection = mongo.meteor.uploads
    data['Synced'] = True
    collection.insert_one(data)


if __name__ == '__main__':
    app.run(host="0.0.0.0", port=80, debug=False)
