/// <reference path="knockout-2.2.1.js" />

ko.bindingHandlers.enable.update = function (element, valueAccessor) {
    var value = ko.utils.unwrapObservable(valueAccessor());
    var $e = $(element);
    if ($e.data('datepicker')) {
        if (value) { $.datepicker._enableDatepicker(element); } else { $.datepicker._disableDatepicker(element); }
    }
    else if ($e.data('timeEntry')) {
        if (value) { $(element).timeEntry('enable'); } else { $(element).timeEntry('disable');; }
    }
    else {
        if (value && element.disabled) element.removeAttribute("disabled");
        else if ((!value) && (!element.disabled)) element.disabled = true;
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var options = allBindingsAccessor().datepickerOptions || {};

        for (var prop in options) {
            if (ko.isSubscribable(options[prop])) {
                var _prop = prop;
                options[prop].subscribe(function (val) {
                    $(element).datepicker('option', _prop, val);
                });
                options[prop] = options[prop]();
            }
        }

        $(element).datepicker($.extend({
            buttonText: '',
            showOn: 'both',
            changeMonth: true,
            changeYear: true
        }, options));

        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).datepicker("getDate"));
        });

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).datepicker("setDate", value);
    }
};

ko.bindingHandlers.numeric = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).numeric(allBindingsAccessor().numeric);

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).removeNumeric();
        });
    }
};

ko.bindingHandlers.time = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var options = allBindingsAccessor().time;

        for (var prop in options) {
            if (ko.isSubscribable(options[prop])) {
                var _prop = prop;
                options[prop].subscribe(function (val) {
                    $(element).timeEntry('option', _prop, val);
                });
                options[prop] = options[prop]();
            }
        }

        $(element).timeEntry($.extend({
            showSeconds: true,
            show24Hours: true,
            spinnerImage: '',
            defaultTime: '00:00:00'
        }, options));

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).timeEntry('destroy');
        });
    }
};
