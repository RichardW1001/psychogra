const ko = require('knockout')

const categoryStore = require('./categoryStore.js')
const arrayHelpers = require('./arrayHelpers.js')

const flatten = function(arr) {
    return arr.reduce(function (flat, toFlatten) {
      return flat.concat(Array.isArray(toFlatten) ? flatten(toFlatten) : toFlatten);
    }, []);
  }

const WhichIsGameViewModel = function () {
    const self = this;

    const size = 4;

    var categories = categoryStore.getCategories();

    var chosenCategory = arrayHelpers.shuffle(categories)[0];

    var chosenItem = arrayHelpers.shuffle(chosenCategory.options)[0];

    var otherCategories = categories.filter(function(c) { return c.categoryId !== chosenCategory.categoryId });

    var otherItems = arrayHelpers.shuffle(flatten(otherCategories.map(function(c) { return c.options; }))).slice(1-size);

    var options = arrayHelpers.shuffle(otherItems.concat([chosenItem]))

    self.mainCategory = chosenCategory;

    self.options = options;

    self.Guess = function (choice) {
        return chosenCategory.options.filter(function(o){
            return o.itemId === choice.itemId;
        }).length > 0
    }

    return self;
}

module.exports = WhichIsGameViewModel;