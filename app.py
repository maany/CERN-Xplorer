#!flask/bin/python
from flask import Flask, jsonify
from flask import abort
from flask import make_response
from flask import request
from flask.ext.httpauth import HTTPBasicAuth

app = Flask(__name__)

users = [
    {
        'id': 1,
        'username': u'sartaj.baveja',
        'particles_collected': u'5', 
        'particles_left': u'4',
        'done' : False
    },
    {
        'id': 2,
        'username': u'rosy.gupta',
        'particles_collected': u'8', 
        'particles_left': u'1',
        'done' : False
    },
    {
    	'id':3,
        'username': u'megha.arora',
        'particles_collected': u'9', 
        'particles_left': u'0',
        'done' : True

    }, 
    {
    	'id':4,
        'username': u'mayank.sharma04',
        'particles_collected': u'9', 
        'particles_left': u'0',
        'done' : False
    }
]

auth = HTTPBasicAuth()

@auth.get_password
def get_password(username):
    if username == 'particle':
        return 'trek'
    return None

@auth.error_handler
def unauthorized():
    return make_response(jsonify({'error': 'Unauthorized access'}), 401)

# curl -i -u particle:trek  http://localhost:5000/api/userDetails
@app.route('/api/userDetails', methods = ['GET'])
@auth.login_required
def get_user_details():
    return jsonify({'users':users})

@app.route('/api/userDetails/<int:user_id>', methods=['GET'])
def get_user(user_id):
	user = [user for user in users if user['id']==user_id]
	if len(user) == 0:
		abort(404)
	return jsonify({'user':user[0]})

@app.errorhandler(404)
def user_not_found(error):
    return make_response(jsonify({'error': 'Not found'}), 404)

# curl -i -H "Content-Type: application/json" -X POST -d '{"username":"mayank.sharma"}' http://localhost:5000/api/userDetails
@app.route('/api/userDetails', methods=['POST'])
def create_user():
    if not request.json or not 'username' in request.json:
        abort(400)
    
    user = {
        'id': users[-1]['id'] + 1,
        'username': request.json['username'],
        'particles_collected': 0,
        'particles_left': 9,
        'done': False
    }
    users.append(user)
    return jsonify({'user': user}), 201

# curl -i -H "Content-Type: application/json" -X PUT -d '{"username":"mayank.sharma04"}' http://localhost:5000/api/userDetails/4
@app.route('/api/userDetails/<int:user_id>', methods=['PUT'])
def update_user_details(user_id):
    user = [user for user in users if user['id'] == user_id]
    if len(user) == 0:
        abort(404)
    if not request.json:
        abort(400)
    if 'username' in request.json and type(request.json['username']) != unicode:
        abort(400)
    user[0]['username'] = request.json.get('username', user[0]['username'])
    return jsonify({'user': user[0]})

@app.route('/api/userDetails/<int:user_id>', methods=['DELETE'])
def delete_user(user_id):
    user = [user for user in users if user['id'] == user_id]
    if len(user) == 0:
        abort(404)
    users.remove(user[0])
    return jsonify({'result': True})

if __name__ == '__main__':
    app.run(debug=True)