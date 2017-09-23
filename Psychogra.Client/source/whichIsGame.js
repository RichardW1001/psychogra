const ko = require('knockout')

const categoryStore = require('./categoryStore.js')
const arrayHelpers = require('./arrayHelpers.js')

const WhichIsGameViewModel = function () {
    const self = this;

    const size = 2;

    var categories = categoryStore.getCategories();

    var chosenCategories = arrayHelpers.shuffle(oddCategory.options).slice(-size)

    self.mainCategory = chosenCategories[0];
    self.options = chosenCategories.slice(1-size).map(function(c) {
        return arrayHelpers.randomElement(c.options)
    })

    self.Guess = function (choice) {
        return mainCategory.options.filter(function(o){
            return o.itemId === choice.itemId;
        }).length > 0
    }

    return self;
}

module.exports = WhichIsGameViewModel;