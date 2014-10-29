@ModelType ServerAutostoppista.RegisterModel
@Code
    ViewData("Title") = "Register"
End Code

<hgroup class="title">
    <h1>@ViewData("Title").</h1>
    <h2>Create a new account.</h2>
</hgroup>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

    @<fieldset>
        <legend>Registration Form</legend>
        <ol>
            <li>
                @Html.LabelFor(Function(m) m.UserName)
                @Html.TextBoxFor(Function(m) m.UserName)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.Name)
                @Html.TextBoxFor(Function(m) m.Name)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.Surname)
                @Html.TextBoxFor(Function(m) m.Surname)
            </li>
             <li>
                @Html.LabelFor(Function(m) m.Kind)
                @Html.DropDownListFor(Function(m) m.Kind, New List(Of SelectListItem) From {New SelectListItem With {.Text = "Passeggero", .Value = ServerAutostoppista.PASSENGER},
                                                                                               New SelectListItem With {.Text = "Autista", .Value = ServerAutostoppista.DRIVER}})
            </li>
            <li>
                @Html.LabelFor(Function(m) m.Password)
                @Html.PasswordFor(Function(m) m.Password)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.ConfirmPassword)
                @Html.PasswordFor(Function(m) m.ConfirmPassword)
            </li>
        </ol>
        <input type="submit" value="Register" />
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
