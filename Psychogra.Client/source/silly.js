(function(){

    const fs = require('fs')
    const join = require('path').join
    const settings = require('electron-settings')
    const ko = require('knockout')

    const req = require('electron-require')

    //const mod1 = require('./source/arrayHelpers.js')

    const shuffle = function (array) {
        var currentIndex = array.length, temporaryValue, randomIndex;
      
        // While there remain elements to shuffle...
        while (0 !== currentIndex) {
      
          // Pick a remaining element...
          randomIndex = Math.floor(Math.random() * currentIndex);
          currentIndex -= 1;
      
          // And swap it with the current element.
          temporaryValue = array[currentIndex];
          array[currentIndex] = array[randomIndex];
          array[randomIndex] = temporaryValue;
        }
      
        return array;
      }
    
    const randomElement = function(items){
        return items[Math.floor(Math.random()*items.length)];
    }

    const getCategories = function(){
        const path = 'C:\\Users\\Richard\\Desktop\\Demo data'
      
        var getItems = function(d){
          return fs.readdirSync(join(path, d)).
            filter(function(f) {
              return !fs.lstatSync(join(path, d, f)).isDirectory()
            }).map(function(f) {
              return {
                itemId: join(path, d, f),
                imageUrl: join(path, d, f),
                label: f
              }
            })
        }
      
        return fs.
          readdirSync(path).
          filter(function(d) {
            return fs.lstatSync(join(path, d)).isDirectory()
          }).
          map(function(d) {
            return {
            categoryId: join(path, d),
            label: d,
            options: getItems(d)
            }
        })
      }

      const OddOneOutGame = function(){
        var self = this;
    
        var size = 4;
    
        var categories = getCategories();
    
        var oddCategory = randomElement(categories)
    
        var mainCategory = randomElement(categories.filter(function(c) {
            return c.categoryId !== oddCategory.categoryId
        }))
    
        var oddOneOut = shuffle(oddCategory.options).slice(-1)
        var others = shuffle(mainCategory.options).slice(1 - size)
    
        var options = others.concat(oddOneOut)
        
        self.options = ko.observableArray(shuffle(options))
    
        self.Guess = function(choice) {
            return choice.itemId === oddOneOut[0].itemId
        }
    
        return self;
    }

    var ViewModel = function() {
        var self = this;

        self.Game = ko.observable();

        var instructions = [
            'Which is not like the others?',
            'Can you find the odd one out?'
        ];

        var instruction = randomElement(instructions);

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
                var message = randomElement(wellDoneMessages);
                window.speechSynthesis.speak(new SpeechSynthesisUtterance(message))
            }
            else{
                var message = randomElement(tryAgainMessages);
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

            return colors[Math.floor(Math.random() * colors.length)];
        }

        return self;
    }

    window.viewModel = new ViewModel();

    ko.applyBindings(window.viewModel);

    window.viewModel.NewGame();
    
})();