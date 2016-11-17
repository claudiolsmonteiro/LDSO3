var promise = require('bluebird');

var options = {
    // Initialization Options
    promiseLib: promise
};

var pgp = require('pg-promise')(options);
pgp.pg.defaults.ssl = true;
//postgres://tvkkymmzveumqn:FbKUj0a9eNPfYZXoqyWrZ_unkw@ec2-54-217-233-192.eu-west-1.compute.amazonaws.com:5432/d2u4fcq0idqldbHeroku CLI
var connectionString = 'postgres://tvkkymmzveumqn:FbKUj0a9eNPfYZXoqyWrZ_unkw@ec2-54-217-233-192.eu-west-1.compute.amazonaws.com:5432/d2u4fcq0idqldb' || 'postgres://localhost:5432/puppies';
var db = pgp(connectionString);

// add query functions

module.exports = {
    getAllPlayers: getAllPlayers,
    getSinglePlayer: getSinglePlayer
    //createPlayer: createPlayer,
    //updatePlayer: updatePlayer,
    //removePlayer: removePlayer
};

function getAllPlayers(req, res, next) {
    db.any('select * from players')
        .then(function (data) {
            res.status(200)
                .json({
                    status: 'success',
                    data: data,
                    message: 'Retrieved ALL players'
                });
        })
        .catch(function (err) {
            return next(err);
        });
}

function getSinglePlayer(req, res, next) {
    var playerID = parseInt(req.params.id);
    db.one('select * from players where id = $1', playerID)
        .then(function (data) {
            res.status(200)
                .json({
                    status: 'success',
                    data: data,
                    message: 'Retrieved ONE player'
                });
        })
        .catch(function (err) {
            return next(err);
        });
}
