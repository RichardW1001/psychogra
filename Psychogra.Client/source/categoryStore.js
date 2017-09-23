const fs = require('fs')
const settings = require('electron-settings')

const getCategories = function(){
  const path = settings.get('rootPath', 'C:\\Users\\Richard\\Desktop\\Demo data')

  var getItems = function(d){
    return fs.readdirSync(d).
      filter(function(f) {
        return !fs.lstatSync(d + f).isDirectory()
      }).map(function(f) {
        return {
          itemId: d + f,
          imageUrl: d + f,
          label: f
        }
      })
  }

  return fs.
    readdirSync(path).
    filter(function(d) {
      return fs.lstatSync(d).isDirectory()
    }).
    map(function(d) {
    return {
      categoryId: path + d,
      label: d,
      options: getItems(d)
    }
  })
}

module.exports = {
  getCategories
}