const fs = require('fs')
const settings = require('electron-settings')
const join = require('path').join

const getCategories = function(){
    const path = settings.get('rootPath')[0];
  
    var getItems = function(d){
      return fs.readdirSync(join(path, d)).
        filter(function(f) {
          return !fs.lstatSync(join(path, d, f)).isDirectory()
        }).map(function(f) {
          return {
            itemId: join(path, d, f),
            imageUrl: join(path, d, f),
            label: f.substr(0, f.lastIndexOf('.'))
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

module.exports = {
    getCategories
}