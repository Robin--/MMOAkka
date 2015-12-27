/*jslint browser: true*/
/*global toastr, $, console*/
(function () {
    'use strict';

    var disableScope = function ($compileProvider) {
        $compileProvider.debugInfoEnabled(false);
    };

    disableScope.$inject = ['$compileProvider'];

    var homeController = function (httpRepository, toaster, $timeout, $scope, $uibModal, $log, $filter) {
        var vm = this;

        vm.messages = [];
        vm.roleOptions = [];


        var loadRoles = function () {
            vm.roleOptions = [];
            httpRepository.getData('api/roles').then(function (data) {
                console.log(data);
                vm.roleOptions = data;
            });
        }

        var loadCharacters = function () {
            vm.characters = [];
            httpRepository.getData('api/all').then(function (data) {
                console.log(data);
                vm.characters = data;
            });
        }

        loadRoles();
        loadCharacters();

        vm.newCharacter = function (name, role) {
            var dataToPost = { name: name, role: role };
            httpRepository.postData('api/create', dataToPost).then(function (data) {
                console.log(data);
                $timeout(function () { loadCharacters() }, 3000);
            });
        }

        vm.renameCharacter = function(id, name) {
            var dataToPost = { id: id, name: name};
            httpRepository.postData('api/'+id+'/rename', dataToPost).then(function (data) {
                console.log(data);
                $timeout(function () { loadCharacters() }, 3000);
            });
        }

        vm.changeRole = function (id, role) {
            var dataToPost = { id: id, role: role };
            httpRepository.postData('api/' + id + '/role', dataToPost).then(function (data) {
                console.log(data);
                $timeout(function () { loadCharacters() }, 3000);
            });
        }
    };

    homeController.$inject = ['httpRepository', 'toaster', '$timeout', '$scope', '$uibModal', '$log', '$filter'];


    var routes = function ($urlRouterProvider, $stateProvider, $locationProvider) {
        $stateProvider.state('home',
            {
                url: '/',
                templateUrl: '/views/home.html'
            });
        $urlRouterProvider.otherwise('/');

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    };

    routes.$inject = ['$urlRouterProvider', '$stateProvider', '$locationProvider'];

    angular.module('mmoApp', ['ngAnimate', 'toaster', 'ui.bootstrap', 'ui.router'])
        .config(disableScope)
        .config(routes)
        .controller('homeController', homeController)
        .filter('getById', function () {
            return function (input, id) {
                var i = 0, len = input.length;
                for (; i < len; i++) {

                    if (input[i].id === id) {
                        return input[i];
                    }
                }
                return null;
            }
        })
        .filter('roleType', function () {
            return function (input) {
                if (input === 0) {
                    return "DPS";
                } else if (input === 1) {
                    return "Healer";
                } else if (input === 2) {
                    return "Tank";
                }
                return null;
            }
        });



}());