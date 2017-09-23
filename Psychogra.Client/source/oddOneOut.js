const ko = require('knockout')

const categoryStore = require('.\categoryStore.js')
const arrayHelpers = require('.\arrayHelpers.js')

const LoadCategories = function(){
    //walk the folders under a root folder
    //each (direct) folder is a category
    //the image files in that folder are items
    //the id can be the full path, as this must be unique

    return categoryStore.getCategories()
}

const OddOneOutGame = function(){
    var self = this;

    var size = 4;

    var categories = categoryStore.getCategories();

    var oddCategory = randomElement(categories)

    var mainCategory = randomElement(categories.filter(function(c) {
        return c.categoryId !== oddCategory.categoryId
    }))

    var oddOneOut = shuffle(oddCategory.options).slice(-1)
    var others = shuffle(mainCategory.options).slice(1 - size)

    var options = others.concat(oddCategory)
    
    self.options = ko.observableArray(shuffle(options))

    self.Guess = function(choice) {
        return choice.choiceId === oddOneOut.choiceId
    }

    return self;
}

module.exports = {
    OddOneOutGame
}