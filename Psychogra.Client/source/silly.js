(function(){

    const settings = require('electron-settings')
    const ko = require('knockout')

    const req = require('electron-require')

    const arrayHelpers = require('./source/arrayHelpers.js')
    const OddOneOutGame = require('./source/oddOneOutGame.js')

    var ViewModel = function() {
        var self = this;

        self.Game = ko.observable();

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
                "That's the one!",
                "Nice!"
            ]

            var result = self.Game().Guess(choice);

            if (result === true){
                var message = arrayHelpers.randomElement(wellDoneMessages);
                window.speechSynthesis.speak(new SpeechSynthesisUtterance(message))
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

        return self;
    }

    window.viewModel = new ViewModel();

    ko.applyBindings(window.viewModel);

    window.viewModel.NewGame();
    
})();