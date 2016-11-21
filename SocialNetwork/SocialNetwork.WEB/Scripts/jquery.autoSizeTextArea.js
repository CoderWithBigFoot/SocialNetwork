/*!
 * autoSizeTextArea v1.0.2
 * Copyright 2015 Julian Hill
 * A jQuery plugin to Auto-size a textarea whenever a key is released or text is pasted.
 * https://autosizetextarea.codeplex.com/
 * Licensed under MIT https://autosizetextarea.codeplex.com/license
 * https://www.nuget.org/packages/jquery.autoSizeTextArea.js/
 */
(function ($) {

    "use strict";

    $.fn.autoSizeTextArea = function (options) {
        /// <signature>
        /// <summary>Auto-sizes a textarea whenever a key is released or text is pasted.</summary>
        /// <param name="options" type="Object">
        /// <para>onResize: A function to call each time the textarea is resized. In the function body, 'this' 
        /// refers to the textarea element.</para>
        /// <para>keyupDelay: The delay, in milliseconds, between a key being released and the textarea being 
        /// resized; specify null for no delay. The default value is 100.</para>
        /// <para>minHeight: The minimum height, in pixels, of the textarea. The default value is null.</para>
        /// </param>
        /// </signature>
        /// <signature>
        /// <summary>Auto-sizes a textarea whenever a key is released or text is pasted.</summary>
        /// <param name="method" type="String">
        /// <para>"update": Resizes the textarea.</para>
        /// <para>"destroy": Reverts the textarea back to its original state.</para>
        /// </param>
        /// </signature>

        var keyupTimeoutId = null;

        var resizeTextarea = function () {

            var $this = $(this);

            var data = $this.data("autoSizeTextArea");
            if (!data) {
                $.error('autoSizeTextArea has never been initialized or has been destroyed.');
            }

            var settings = data.settings;
            
            var lastHeight = parseFloat($this.css("height"));

            var paddingTop, paddingBottom, paddingHeight, scrollHeight, clientHeight,
                borderTop, borderBottom, borderHeight, height,
                scrollTop = $(window).scrollTop();

            keyupTimeoutId = 0;

            // IE8 textareas have the onpage property - others do not
            if (!("onpage" in this)) {
                $this.css({
                    "height": 0,
                    "min-height": 0,
                    "max-height": 0
                });
            }

            scrollHeight = this.scrollHeight;
            clientHeight = this.clientHeight;
            borderTop = parseFloat($this.css("border-top-width"));
            borderBottom = parseFloat($this.css("border-bottom-width"));
            borderHeight = borderTop + borderBottom;
            height = scrollHeight + borderHeight;

            // For FF
            if (clientHeight === 0) {
                paddingTop = parseFloat($this.css("padding-top"));
                paddingBottom = parseFloat($this.css("padding-bottom"));
                paddingHeight = paddingTop + paddingBottom;
                height += paddingHeight;
            }

            if (settings.minHeight) {
                height = Math.max(settings.minHeight, height);
            }

            $this.css({
                "height": height,
                "min-height": "",
                "max-height": ""
            });

            $(window).scrollTop(scrollTop);

            if (lastHeight !== height) {
                if (settings.onResize) {
                    settings.onResize.apply(this);
                }
            }
        };

        // <= IE9 Can't pass arguments with the timeout callback function
        var _delay = function (handler, delay) {
            var instance;
            var handlerProxy = function () {
                return handler.apply(instance, arguments);
            };
            instance = this;
            return setTimeout(handlerProxy, delay || 0);
        };

        var callOrDelay = function (e) {
            var me = this, settings;

            if (e.type === "paste" || e.type === "cut") {
                setTimeout(function () {
                    if (e.type === "paste") {
                        settings = $(me).data("autoSizeTextArea").settings;
                        if (settings.onPasted) {
                            settings.onPasted.apply(me);
                        }
                    }
                    resizeTextarea.apply(me);
                }, 100);
            }
            else if (e.type === "change") {
                resizeTextarea.apply(me);
            }
            else {
                settings = $(this).data("autoSizeTextArea").settings;
                if (keyupTimeoutId) {
                    clearTimeout(keyupTimeoutId);
                }
                var delayms = settings.keyupDelay;
                if (delayms) {
                    delayms = parseInt(delayms);
                }
                if (delayms) {
                    keyupTimeoutId = _delay.call(this, resizeTextarea, delayms);
                }
                else {
                    resizeTextarea.apply(this);
                }
            }
        };

        var methods = {
            update: function () {
                return this.each(resizeTextarea);
            },
            destroy: function () {
                this.each(function () {
                    var $this = $(this);
                    var data = $this.data("autoSizeTextArea");
                    // Ensure no error if destroy twice
                    if (data) {
                        $this.unbind("keyup cut paste change", data.boundEvents)
                             .removeData("autoSizeTextArea");
                    }
                });
            }
        };
        
        if (methods[options]) {
            return methods[options].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof options === 'object' || !options) {
            this.each(function () {
                $(this).bind("keyup cut paste change", callOrDelay)
                       .data("autoSizeTextArea", { 
                           settings: $.extend({}, $.fn.autoSizeTextArea.defaults, options),
                           boundEvents: callOrDelay 
                       });
            });
        } else {
            $.error('autoSizeTextArea does not contain the method: ' + options);
        }
    };
    
    $.fn.autoSizeTextArea.defaults = {
        /// <field>A function to call each time the textarea is resized. In the function body, 'this' 
        /// refers to the textarea element.
        /// <para>&#160;</para>
        /// <para>Default value is null.</para>
        /// </field>
        onResize: null,
        /// <field type='Number' integer='true'>The delay, in milliseconds, between a key being released and the textarea 
        /// being resized; specify null for no delay.
        /// <para>&#160;</para>
        /// <para>Default value is 100.</para>
        /// </field>
        keyupDelay: 100,
        /// <field type='Number' integer='true'>The minimum height, in pixels, of the textarea.
        /// <para>&#160;</para>
        /// <para>Default value is null.</para>
        /// </field>
        minHeight: null,
        /// <field>A function to call after text has been pasted into the textarea. In the function body, 'this' 
        /// refers to the textarea element.
        /// <para>&#160;</para>
        /// <para>Default value is null.</para>
        /// </field>
        onPasted: null
    };

})(jQuery);