require.config({
    baseUrl: 'Scripts',
    paths: {
        "jquery": "jquery-1.10.2",
        "bootstrap": "bootstrap.min",
        "knockout": "knockout-2.3.0",
        "knockout.validation": "knockout.validation",
        
        
        "sammy": "sammy-0.7.4",
        "ajaxPrefilters": "app/ajaxPrefilters",
        "app.bindings": "app/app.bindings",
        "app.datamodel": "app/app.datamodel",
        "app.viewmodel": "app/app.viewmodel",
        "home.viewmodel": "app/home.viewmodel",
        "login.viewmodel": "app/login.viewmodel",
        "register.viewmodel": "app/register.viewmodel",
        "registerExternal.viewmodel": "app/registerExternal.viewmodel",
        "manage.viewmodel": "app/manage.viewmodel",
        "userInfo.viewmodel": "app/userInfo.viewmodel"
    },
    shim: {
        "bootstrap": ["jquery"],
        "knockout.validation": ["knockout"]
    }
});

require(["knockout"], function (ko) {
    //manually set the global ko property
    window.ko = ko;

    //then bring in knockout validation
    require(["jquery", "knockout", "app.viewmodel", "knockout.validation", "domReady!"], function ($, k, app, kovalidation) {

        // Activate Knockout
        ko.validation.init({ grouping: { observable: false } });
        ko.applyBindings(app);
    });
});
