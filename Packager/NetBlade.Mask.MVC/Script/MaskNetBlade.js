window.NetBlade || (window.NetBlade = {});
window.NetBlade.Package || (window.NetBlade.Package = {});

window.NetBlade.Package.mask = (function () {

    function mask() {

        this.config = {
            clearIfNotMatch: true,
            maxlength: false,
        };

        $(document).ready(function (_this) {
            return function () {
                _this.bindElements();
            }
        }(this));

        $(document).ajaxComplete(function (_this) {
            return function (event, xhr, settings) {
                _this.bindElements();
            }
        }(this));
    }

    mask.prototype.bindElements = function () {
        var elements = $(document.body).find('input[data-mask-templete]');
        elements.each(function (_this) {
            return function (index, element) {
                _this.applyMask($(element));
            }
        }(this));
    };

    mask.prototype.applyMask = function (element) {
        var options = {

            onChange: function (_this) {
                return function (val, e, el, options) {
                    return _this.onChange(val, e, el, options);
                }
            }(this),

            onKeyPress: function (_this) {
                return function (val, e, el, options) {
                    return _this.onKeyPress(val, e, el, options);
                }
            }(this),

            onComplete: function (_this) {
                return function (val, e, el, options) {
                    return _this.onComplete(val, e, el, options);
                }
            }(this),

            onInvalid: function (_this) {
                return function (val, e, f, invalid, options) {
                    return _this.onInvalid(val, e, f, invalid, options);
                }
            }(this),

            clearIfNotMatch: this.config.clearIfNotMatch,
            maxlength: this.config.maxlength,
            placeholder: null,
        };

        element.mask(function (_this) {
            return function (val) {
                return _this.maskBehavior(val, null, element, null, options);
            }
        }(this), options);

    };

    mask.prototype.maskBehavior = function (val, e, el, invalid, options) {
        var masks = el.data('mask-templete');
        var mask = null;
        var currentMask = el.data('mask-templete-current');

        if (masks.length === 1 || !val || val === '') {
            mask = masks[0];
            el.data('mask-templete-current', mask)
        }
        else {
            $.each(masks, function (_this) {
                return function (index, item) {
                    if (item.length >= val.length && mask === null) {
                        mask = item;
                        el.data('mask-templete-current', mask);
                    }
                }
            }(this));
        }

        return mask || currentMask;
    };

    mask.prototype.onChange = function (val, e, el, options) {
        //var $this = this;
    };

    mask.prototype.onInvalid = function (val, e, f, invalid, options) {
        //el.mask(this.maskBehavior(currVal, e, el, null, options), options);
    };

    mask.prototype.onKeyPress = function (val, e, el, options) {
        //el.mask(this.maskBehavior(val, e, el, null, options), options);
    };

    mask.prototype.onComplete = function (val, e, el, options) {
        //var mask = this.maskBehavior(currVal, e, el, null, options);
        //el.mask(mask, options);
    };

    return mask;
})();