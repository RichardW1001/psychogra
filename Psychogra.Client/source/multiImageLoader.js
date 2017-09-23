(function () {

    const settings = require('electron-settings')
    const ko = require('knockout')

    const req = require('electron-require')

    const arrayHelpers = require('./source/arrayHelpers.js')
    const MultiImageGame = require('./source/multiImageGame.js')

    var ViewModel = function () {
        var self = this;

        self.Game = ko.observable();
        self.Instruction = ko.observable();

        self.ReadInstruction = function(){
            window.speechSynthesis.speak(new SpeechSynthesisUtterance(self.Instruction()))
        }

        self.NewGame = function () {
            self.Game(new MultiImageGame())

            var item = self.Game().mainItem().label;

            var instructions = [
                'Which is the ' + item + '?',
                'Can you find the' + item + '?'
            ];

            var instruction = arrayHelpers.randomElement(instructions);

            self.Instruction(instruction);
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

            if (result === true) {
                var message = arrayHelpers.randomElement(wellDoneMessages);
                window.speechSynthesis.speak(new SpeechSynthesisUtterance(message))
            }
            else {
                var message = arrayHelpers.randomElement(tryAgainMessages);
                window.speechSynthesis.speak(new SpeechSynthesisUtterance(message))
            }
        }

        self.RandomColor = function () {
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