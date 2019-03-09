using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

public static class HtmlHelperExtensions
{
    public static IHtmlString koSelectSimple(this HtmlHelper html, string value, string list)
    {
        return html.Raw(string.Format(@"<select data-bind=""value: {0}, options: {1}, optionsCaption: '[SELECCIONAR]', uniqueName: true"" data-val=""true"" data-val-required=""""></select>", value, list));
    }


    public static IHtmlString koSelect(this HtmlHelper html, string value, string list, bool requerido = true)
    {
        return html.Raw(string.Format(@"<select data-bind=""value: {0}, options: {1}, optionsText: 'nombre', optionsValue: 'id', optionsCaption: '[SELECCIONAR]', uniqueName: true"" data-val=""true"" " + ((requerido)?"data-val-required=\"\"":"") + "></select>", value, list));
    }

    public static IHtmlString koSelectV2(this HtmlHelper html, string value, string list, string requerido)
    {
        return html.Raw(string.Format(@"<select data-bind=""value: {0}, options: {1}, optionsText: 'nombre', optionsValue: 'id', optionsCaption: '[SELECCIONAR]', uniqueName: true"" data-val=""true"" " + (( Convert.ToBoolean(requerido)) ? "data-val-required=\"\"" : "") + "></select>", value, list));
    }
    public static IHtmlString koSelectV3(this HtmlHelper html, string value, string list, string enable, bool requerido = true)
    {
        return html.Raw(string.Format(@"<select data-bind=""value: {0}, options: {1}, optionsText: 'nombre', optionsValue: 'login', optionsCaption: '[SELECCIONAR]', enable: {2}, uniqueName: true"" data-val=""true"" " + ((requerido) ? "data-val-required=\"\"" : "") + "></select>", value, list, enable));
    }

    public static IHtmlString koSelect(this HtmlHelper html, string value, string list, string enable, bool requerido = true)
    {
        return html.Raw(string.Format(@"<select data-bind=""value: {0}, options: {1}, optionsText: 'nombre', optionsValue: 'id', optionsCaption: '[SELECCIONAR]', enable: {2}, uniqueName: true"" data-val=""true"" " + ((requerido)?"data-val-required=\"\"":"") + "></select>", value, list, enable));
    }


    public static IHtmlString koSelectNoRequired(this HtmlHelper html, string value, string list)
    {
        return html.Raw(string.Format(@"<select data-bind=""value: {0}, options: {1}, optionsText: 'nombre', optionsValue: 'id', optionsCaption: '[SELECCIONAR]', uniqueName: true""></select>", value, list));
    }

    public static IHtmlString koSelectNoRequired(this HtmlHelper html, string value, string list, string enable)
    {
        return html.Raw(string.Format(@"<select data-bind=""value: {0}, options: {1}, optionsText: 'nombre', optionsValue: 'id', optionsCaption: '[SELECCIONAR]', enable: {2}, uniqueName: true""></select>", value, list, enable));
    }

    public static IHtmlString koCheck(this HtmlHelper html, string value)
    {
        return html.Raw(string.Format(@"<input type=""checkbox"" data-bind=""checked: {0}, uniqueName: true"" />", value));
    }

    public static IHtmlString koCheck(this HtmlHelper html, string value, string enable)
    {
        return html.Raw(string.Format(@"<input type=""checkbox"" data-bind=""checked: {0}, enable: {1}, uniqueName: true"" />", value, enable));
    }

    public static IHtmlString koText(this HtmlHelper html, string value, int? maxlength = null)
    {
        return html.Raw(string.Format(@"<input type=""text"" data-bind=""value: {0}, uniqueName: true, valueUpdate: 'afterkeydown'"" data-val=""true"" data-val-required="""" {1}/>", value, ((maxlength)==null)?"":("maxlength=\"" + maxlength.Value + "\"") ));
    }

    public static IHtmlString koText(this HtmlHelper html, string value, string enable)
    {
        return html.Raw(string.Format(@"<input type=""text"" data-bind=""value: {0}, enable: {1}, uniqueName: true, valueUpdate: 'afterkeydown'"" data-val=""true"" data-val-required="""" />", value, enable));
    }

    public static IHtmlString koTextNoRequired(this HtmlHelper html, string value, int? maxlength = null, string tooltip = null)
    {
        return html.Raw(string.Format(@"<input type=""text"" data-bind=""value: {0}, uniqueName: true, valueUpdate: 'afterkeydown'"" {1} {2}/>", value, ((maxlength) == null) ? "" : ("maxlength=\"" + maxlength.Value + "\""), ((tooltip) == null) ? "" : ("title=\"" + tooltip + "\"")));
    }

    public static IHtmlString koTextNoRequired(this HtmlHelper html, string value, string enable, int? maxlength = null)
    {
        return html.Raw(string.Format(@"<input type=""text"" data-bind=""value: {0}, enable: {1}, uniqueName: true, valueUpdate: 'afterkeydown'"" {2}/>", value, enable, ((maxlength) == null) ? "" : ("maxlength=\"" + maxlength.Value + "\"")));
    }

    public static IHtmlString koTextAreaNoRequired(this HtmlHelper html, string value, int? maxlength = null)
    {
        return html.Raw(string.Format(@"<textarea data-bind=""value: {0}, uniqueName: true, valueUpdate: 'afterkeydown'""  {1}></textarea>", value, ((maxlength) == null) ? "" : ("maxlength=\"" + maxlength.Value + "\"")));
    }

    public static IHtmlString koTextAreaNoRequired(this HtmlHelper html, string value, int? maxlength = null, string tooltip=null)
    {
        return html.Raw(string.Format(@"<textarea data-bind=""value: {0}, uniqueName: true, valueUpdate: 'afterkeydown'""  {1} {2}></textarea>", value, ((maxlength) == null) ? "" : ("maxlength=\"" + maxlength.Value + "\""), ((tooltip) == null) ? "" : ("title=\"" + tooltip + "\"")));
    }

    public static IHtmlString koTextArea(this HtmlHelper html, string value, int? maxlength = null)
    {
        return html.Raw(string.Format(@"<textarea data-bind=""value: {0}, uniqueName: true, valueUpdate: 'afterkeydown'"" data-val=""true"" data-val-required="""" {1}></textarea>", value, ((maxlength) == null) ? "" : ("maxlength=\"" + maxlength.Value + "\"")));
    }

    public static IHtmlString koTextArea(this HtmlHelper html, string value, string enable)
    {
        return html.Raw(string.Format(@"<textarea data-bind=""value: {0}, enable: {1}, uniqueName: true, valueUpdate: 'afterkeydown'"" data-val=""true"" data-val-required=""""></textarea>", value, enable));
    }

    public static IHtmlString koDate(this HtmlHelper html, string value, string enable = null, string disable = null, string min = null, string max = null, int? width = null, bool required = true)
    {
        if (enable != null && disable != null) throw new Exception("No se puede utilizar 'enable' y 'disable' a la vez.");

        var attr = new List<string>();
        if (required)
        {
            attr.Add("data-val='true'");
            attr.Add("data-val-required=''");
        }

        var databind = new List<string>();
        databind.Add("datepicker: " + value);
        databind.Add("uniqueName: true");
        if (enable != null) databind.Add("enable: " + enable);
        if (disable != null) databind.Add("disable: " + disable);

        var options = new List<string>();
        if (min != null) options.Add("minDate: " + min);
        if (max != null) options.Add("maxDate: " + max);
        if (options.Count > 0) databind.Add("datepickerOptions: { " + string.Join(", ", options) + " }");

        var style = new List<string>();
        if (width.HasValue) style.Add("width: " + width + "px");


        attr.Add("data-bind=\"" + string.Join(", ", databind) + "\"");
        if (style.Count > 0) attr.Add("style=\"" + string.Join("; ", style) + "\"");

        return html.Raw("<input type='text' " + string.Join(" ", attr) + "  maxlength='10'/>");
    }

    public static IHtmlString koNumeric(this HtmlHelper html, string value, string enable = null, string disable = null, bool @decimal = true, bool negative = false, int? width = null, bool required = true, int? minlength = null, int? maxlength = null, decimal? min = null, decimal? max = null)
    {
        if (enable != null && disable != null) throw new Exception("No se puede utilizar 'enable' y 'disable' a la vez.");

        var attr = new List<string>();
        if (required || min.HasValue || max.HasValue)
        {
            attr.Add("data-val='true'");
            if (required) attr.Add("data-val-required=''");
            if (min.HasValue || max.HasValue)
            {
                attr.Add("data-val-range=''");
                if (min.HasValue) attr.Add("data-val-range-min='" + min.Value.ToString() + "'");
                if (max.HasValue) attr.Add("data-val-range-max='" + max.Value.ToString() + "'");
            }
        }

        if (maxlength.HasValue)
        {
            attr.Add("maxlength=\"" + maxlength.Value + "\"");
        }
        if (minlength.HasValue)
        {
            attr.Add("minlength=\"" + minlength.Value + "\"");
        }
        var databind = new List<string>();
        databind.Add("value: " + value);
        databind.Add("uniqueName: true");
        if (enable != null) databind.Add("enable: " + enable);
        if (disable != null) databind.Add("disable: " + disable);

        var options = new List<string>();
        if (!@decimal) options.Add("decimal: false");
        if (!negative) options.Add("negative: false");
        databind.Add("numeric: { " + string.Join(", ", options) + " }");

        var style = new List<string>();
        style.Add("text-align: right");
        if (width.HasValue) style.Add("width: " + width + "px");

        attr.Add("data-bind=\"" + string.Join(", ", databind) + "\"");
        attr.Add("style=\"" + string.Join("; ", style) + "\"");

        return html.Raw("<input type='text' " + string.Join(" ", attr) + " />");
    }

    public static IHtmlString koTime(this HtmlHelper html, string value, string enable = null, string disable = null, string min = null, string max = null, int? width = 60, bool required = true)
    {
        if (enable != null && disable != null) throw new Exception("No se puede utilizar 'enable' y 'disable' a la vez.");

        var attr = new List<string>();
        if (required)
        {
            attr.Add("data-val='true'");
            attr.Add("data-val-required=''");
        }

        var databind = new List<string>();
        databind.Add("value: " + value);
        databind.Add("uniqueName: true");
        if (enable != null) databind.Add("enable: " + enable);
        if (disable != null) databind.Add("disable: " + disable);

        var options = new List<string>();
        if (min != null) options.Add("minTime: " + min);
        if (max != null) options.Add("maxTime: " + max);
        databind.Add("time: { " + string.Join(", ", options) + " }");

        var style = new List<string>();
        if (width.HasValue) style.Add("width: " + width + "px");

        attr.Add("data-bind=\"" + string.Join(", ", databind) + "\"");
        if (style.Count > 0) attr.Add("style=\"" + string.Join("; ", style) + "\"");

        return html.Raw("<input type='text' " + string.Join(" ", attr) + " />");
    }

    public static IHtmlString koChkVal(this HtmlHelper html, string condition)
    {
        return html.Raw("<input type='text' readonly class='chkval' data-val='true' data-val-length='' data-val-length-max='0' data-bind='value: msjChkVal.toString(), uniqueName: true, disable: " + condition + "' />");
    }

    public static IHtmlString koChkNumeric(this HtmlHelper html, string condition)
    {
        return html.Raw("<input type='text' readonly class='chkval' data-val='true' data-val-length='' data-val-length-max='0' data-bind='value: msjChkNumeric.toString(), uniqueName: true, disable: " + condition + "' />");
    }
}