const ko = require('knockout')

const categoryStore = require('./categoryStore.js')
const arrayHelpers = require('./arrayHelpers.js')

const MultiImageGameViewModel = function () {
    const self = this;

    const size = 4;

    var categories = categoryStore.getCategories();

    var category = arrayHelpers.randomElement(categories)

    var options = arrayHelpers.shuffle(category.options).splice(-size)

    var mainItem = options[0]

    self.mainItem = ko.observable(mainItem);
    self.options = ko.observableArray(arrayHelpers.shuffle(options))

    self.Guess = function (choice) {
        return choice.itemId === mainItem.itemId
    }

    return self;
}

module.exports = MultiImageGameViewModel;