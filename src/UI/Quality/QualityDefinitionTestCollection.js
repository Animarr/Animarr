var Backbone = require('backbone');
var PagableCollection = require('backbone.pageable');

var _ = require('underscore');
var QualityDefinitionModel = require('./QualityDefinitionTestModel');
var AsSortedCollection = require('../Mixins/AsSortedCollection');

var Collection = PagableCollection.extend({
    model : QualityDefinitionModel,
    url   : window.NzbDrone.ApiRoot + '/qualitydefinition/test',
    bestMatch : undefined,
    parse: function(response) {
        this.bestMatch = response.bestMatch;
        return response.matches;
    },

    state : {
        pageSize : 2000,
        sortKey  : 'matches',
        order    : 1
    },

    mode : 'client',

    sortMappings : {
        'matches' : {
            sortValue : function(model) {
                var matches = model.get("matches");
                var weight = 0;
                _.each(matches, function(value, key){
                    if (value === true) {
                        weight += 1;
                    }
                });
                return weight;
            }
        }
    }
});

var SortedCollection = AsSortedCollection.call(Collection);
module.exports = SortedCollection;