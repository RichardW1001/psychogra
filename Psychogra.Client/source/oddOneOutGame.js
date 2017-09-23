const ko = require('knockout')

const categoryStore = require('./categoryStore.js')
const arrayHelpers = require('./arrayHelpers.js')

const OddOneOutGame = function(){
    var self = this;

    var size = 4;

    var categories = categoryStore.getCategories();

    var oddCategory = arrayHelpers.randomElement(categories)

    var mainCategory = arrayHelpers.randomElement(categories.filter(function(c) {
        return c.categoryId !== oddCategory.categoryId
    }))

    var oddOneOut = arrayHelpers.shuffle(oddCategory.options).slice(-1)
    var others = arrayHelpers.shuffle(mainCategory.options).slice(1 - size)

    var options = others.concat(oddOneOut)
    
    self.options = ko.observableArray(arrayHelpers.shuffle(options))

    self.Guess = function(choice) {
        return choice.itemId === oddOneOut[0].itemId
    }

    return self;
}

module.exports = OddOneOutGame;