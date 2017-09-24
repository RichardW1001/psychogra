(function(){

    const settings = require('electron-settings')
    const ko = require('knockout')

    const req = require('electron-require')

    const arrayHelpers = require('./source/arrayHelpers.js')
    const OddOneOutGame = require('./source/oddOneOutGame.js')

    var ViewModel = function() {
        var self = this;

        self.Game = ko.observable();
        self.WellDoneMessage = ko.observable();

        var instructions = [
            'Which is not like the others?',
            'Can you find the odd one out?'
        ];

        var instruction = arrayHelpers.randomElement(instructions);

        self.Instruction = ko.observable(instruction)

        self.ReadInstruction = function(){
            window.speechSynthesis.speak(new SpeechSynthesisUtterance(self.Instruction()))
        }

        self.NewGame = function() {
            self.Game(new OddOneOutGame())
        }

        self.ChooseOption = function (choice) {

            var tryAgainMessages = [
                'Not quite, try again',
                'Have another go',
                "That's not it"
            ]

            var wellDoneMessages = [
                'Well done!',
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

        self.Color = function(index){
            var colors = [
                'yellow',
                'red',
                'blue',
                'green'
            ];

            return colors[index];
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