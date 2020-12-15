from flask import Flask, make_response, jsonify
import os

app = Flask(__name__)

@app.route("/api/auth", methods=['GET'])
def auth():
    # return make_response(jsonify({'error': 'Unauthorized'}), 401)
    return make_response(jsonify({'auth': 'ok'}), 200)

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=80, debug=False)
