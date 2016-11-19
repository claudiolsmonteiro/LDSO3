var express = require('express');
var router = express.Router();

var db = require('../queries');


router.get('/api/players', db.getAllPlayers);
router.get('/api/players/:id', db.getSinglePlayer);
router.post('/api/players', db.createPlayer);
router.post('/api/players/score', db.createScore);
//router.delete('/api/players/:id', db.removePlayer);


module.exports = router;