var dateTimeManager = {
    init: function () {
        this.handleUtcDates();

        this.initDatePickers();

        $(document).on('dp.change', '.js-datetimepicker', function (e) {
            if ($(e.currentTarget).hasClass('js-linked-picker'))
                dateTimeManager.updateLinkedDates(e.currentTarget);
            dateTimeManager.updateHiddenField(e.currentTarget);
        });

        $('.js-linked-picker:not([data-linked-id])').each(function (i, el) {
            $(el).data('DateTimePicker').date(new moment(new Date()).add(1, 'h'));
        });

        $(document).ajaxSuccess(function () {
            dateTimeManager.handleUtcDates();
        });
    },

    initDatePickers: function (opts) {
        $('.js-datetimepicker').each(function (i, el) {
            var value = $(el).val();
            var date = value ? new moment(value) : dateTimeManager.calculateLinkedDate(new moment(), el);
            opts = Object.assign({
                locale: 'ru',
                format: 'DD.MM.YYYY HH:mm'
            }, opts);

            var dt = $(el).datetimepicker(opts);
            dt.data('DateTimePicker').defaultDate(date);
        });
    },

    updateLinkedDates: function (input) {
        var $el = $(input);
        var selectedDate = $el.data("DateTimePicker").date().clone();
        var mainFieldId = $el.data('main-field-id');

        $('#' + mainFieldId).val(selectedDate.format('YYYY-MM-DDTHH:mm:ss'));
        $('.js-linked-picker[data-linked-id="' + $el.attr('id') + '"]').each(function (i, el) {
            var newDate = dateTimeManager.calculateLinkedDate(selectedDate.clone(), el);

            $(el).data("DateTimePicker").date(newDate);
            $(el).data("DateTimePicker").minDate(newDate);
        });

    },

    updateHiddenField: function (input) {
        var $el = $(input);
        if (!$el.data("DateTimePicker"))
            return;

        var selectedDate = $el.data("DateTimePicker").date().clone();
        var hidden = $el.nextAll('input:hidden');

        hidden.val(selectedDate.format('YYYY-MM-DDTHH:mm:ss'));
    },

    calculateLinkedDate: function (date, input) {
        var stepHours = $(input).data('step-hours');
        var stepMinutes = $(input).data('step-minutes');

        if (stepHours)
            date.add(stepHours, 'h');
        if (stepMinutes)
            date.add(stepMinutes, 'm');

        return date;
    },

    handleUtcDates: function () {
        $('.js-utc-input, .js-utc-html').each(function (i, el) {
            var isHtml = $(el).hasClass('js-utc-html');

            var text = isHtml ? $(el).html() : $(el).val();

            var date = new moment(new Date(text));
            if (!date.isValid())
                return console.log('"' + text + '"' + ' is not valid date')

            isHtml ? $(el).html(date.format("DD.MM.YY HH:mm")) : $(el).val(date.format("DD.MM.YY HH:mm"));
        });
    },

    getFormData: function ($form) {
        var unindexed_array = $form.serializeArray();
        var indexed_array = {};

        $.map(unindexed_array, function (n, i) {
            indexed_array[n['name']] = n['value'];
        });

        return indexed_array;
    }
}