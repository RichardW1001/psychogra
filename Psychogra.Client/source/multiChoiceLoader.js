(function(){
    
        const settings = require('electron-settings')
        const ko = require('knockout')
    
        const req = require('electron-require')
    
        const arrayHelpers = require('./source/arrayHelpers.js')
        const MultiImageGame = require('./source/multiImageGame.js')
    
        var ViewModel = function() {
            var self = this;
    
            self.Game = ko.observable();
    
            var instructions = [
                'What is in the picture?'
            ];
    
            var instruction = arrayHelpers.randomElement(instructions);
    
            self.Instruction = ko.observable(instruction);
            self.WellDoneMessage = ko.observable();
    
            self.ReadInstruction = function(){
                window.speechSynthesis.speak(new SpeechSynthesisUtterance(self.Instruction()))
            }
    
            self.NewGame = function() {
                self.Game(new MultiImageGame())
            }
    
            self.ChooseOption = function (choice) {
    
                var tryAgainMessages = [
                    'Not quite, try again',
                    'Have another go',
                    "That's not it"
                ]
    
                var wellDoneMessages = [
                    'Well done!',
                    "That's it!",
                    "Nice!"
                ]
    
                var result = self.Game().Guess(choice);
    
                if (result === true){
                    var sound = new Audio('./sounds/Ta Da-SoundBible.com-1884170640.wav');
                    sound.play();
    
                    var message = arrayHelpers.randomElement(wellDoneMessages);
                    self.WellDoneMessage(message);
    
                    setTimeout(function(){
                        self.WellDoneMessage(undefined);
                    }, 5000)
                }
                else{
                    var message = arrayHelpers.randomElement(tryAgainMessages);
                    window.speechSynthesis.speak(new SpeechSynthesisUtterance(message))
                }
            }
    
            self.RandomColor = function() {
                var colors = [
                    'orange',
                    'red',
                    'blue',
                    'green',
                    'gray',
                    'pink'
                ];
    
                return arrayHelpers.randomElement(colors);
            }

            self.Menu = function(){
                window.location = 'index.html'
            }
    
            return self;
        }
    
        window.viewModel = new ViewModel();
    
        ko.applyBindings(window.viewModel);
    
        window.viewModel.NewGame();
        
    })();