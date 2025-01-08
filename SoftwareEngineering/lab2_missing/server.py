from flask import Flask, request, jsonify
from flask_cors import CORS

app = Flask(__name__)
CORS(app)

# In-memory storage for words
words = set()

@app.route('/words', methods=['GET'])
def get_words():
    return jsonify(list(words))

@app.route('/words/<string:word>', methods=['DELETE'])
def delete_word(word):
    if word in words:
        words.remove(word)
        return jsonify({"message": f"Word '{word}' deleted successfully"}), 200
    return jsonify({"error": "Word not found"}), 404

@app.route('/words', methods=['POST'])
def add_word():
    data = request.get_json()
    word = data.get('word')
    if word:
        words.add(word)
        return jsonify({"message": "Word added successfully"}), 201
    return jsonify({"error": "Invalid input"}), 400

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=1234)
