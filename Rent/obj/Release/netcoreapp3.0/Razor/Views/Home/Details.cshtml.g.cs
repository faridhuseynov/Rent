#pragma checksum "D:\VS\Rent\Rent\Views\Home\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4e5c0045a87e6b53ba331f47188c5688852a5c2e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Details), @"mvc.1.0.view", @"/Views/Home/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\VS\Rent\Rent\Views\_ViewImports.cshtml"
using Rent;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\VS\Rent\Rent\Views\_ViewImports.cshtml"
using Rent.DomainModels.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\VS\Rent\Rent\Views\_ViewImports.cshtml"
using Rent.ViewModels.ProductViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e5c0045a87e6b53ba331f47188c5688852a5c2e", @"/Views/Home/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56034a03e3fd022f8fb82d49d7e1b5485f27e8a8", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Images/default-pro.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SendProposal", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("add-to-cart-btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("review-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- SECTION -->
<div class=""section"">
    <!-- container -->
    <div class=""container"">
        <!-- row -->
        <div class=""row"">
            <!-- Product main img -->
            <div class=""col-md-5 col-md-push-2"">
                <div id=""product-main-img"">
");
#nullable restore
#line 15 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                 if (Model.Images.Count>0)
                {
                    foreach (var image in Model.Images)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"product-preview\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4e5c0045a87e6b53ba331f47188c5688852a5c2e6729", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 590, "~/Images/", 590, 9, true);
#nullable restore
#line 20 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
AddHtmlAttributeValue("", 599, image.PhotoUrl, 599, 15, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n");
#nullable restore
#line 22 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                    }
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"product-preview\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4e5c0045a87e6b53ba331f47188c5688852a5c2e8604", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n");
#nullable restore
#line 29 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n            <!-- /Product main img -->\r\n            <!-- Product thumb imgs -->\r\n            <div class=\"col-md-2  col-md-pull-5\">\r\n                <div id=\"product-imgs\">\r\n");
#nullable restore
#line 36 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                     if (Model.Images.Count > 0)
                    {
                        foreach (var image in Model.Images)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"product-preview\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4e5c0045a87e6b53ba331f47188c5688852a5c2e10564", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1386, "~/Images/", 1386, 9, true);
#nullable restore
#line 41 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
AddHtmlAttributeValue("", 1395, image.PhotoUrl, 1395, 15, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n");
#nullable restore
#line 43 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                        }
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"product-preview\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4e5c0045a87e6b53ba331f47188c5688852a5c2e12472", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n");
#nullable restore
#line 50 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                  \r\n");
            WriteLiteral("                </div>\r\n            </div>\r\n            <!-- /Product thumb imgs -->\r\n            <!-- Product details -->\r\n            <div class=\"col-md-5\">\r\n                <div class=\"product-details\">\r\n                    <h2 class=\"product-name\">");
#nullable restore
#line 61 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                                        Write(Model.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                    <h3>Məhsulun sahibi: ");
#nullable restore
#line 62 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                                    Write(Model.User.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                    <div>
                        <div class=""product-rating"">
                            <i class=""fa fa-star""></i>
                            <i class=""fa fa-star""></i>
                            <i class=""fa fa-star""></i>
                            <i class=""fa fa-star""></i>
                            <i class=""fa fa-star-o""></i>
                        </div>
                        <a class=""review-link"" href=""#"">10 Review(s) | Add your review</a>
                    </div>
                    <div>
                        <h3 class=""product-price"">");
#nullable restore
#line 74 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                                             Write(Model.ProductPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span>AZN/gun</span> <del class=\"product-old-price\">");
#nullable restore
#line 74 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                                                                                                                     Write(Model.ProductPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</del></h3>\r\n                        <span class=\"product-available\">In Stock</span>\r\n                    </div>\r\n                    <p>");
#nullable restore
#line 77 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                  Write(Model.ProductDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4e5c0045a87e6b53ba331f47188c5688852a5c2e16185", async() => {
                WriteLiteral("\r\n                        <input hidden name=\"Id\"");
                BeginWriteAttribute("value", " value=\"", 3187, "\"", 3204, 1);
#nullable restore
#line 79 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
WriteAttributeValue("", 3195, Model.Id, 3195, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                        <input hidden");
                BeginWriteAttribute("value", " value=\"", 3247, "\"", 3274, 1);
#nullable restore
#line 80 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
WriteAttributeValue("", 3255, User.Identity.Name, 3255, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" name=""buyer"" />
                        <div class=""add-to-cart"">
                            <div class=""qty-label"">
                                AZN/gun
                                <div class=""input-number"">
                                    <input type=""number"" name=""ProposedPrice"" id=""proposedPrice"" value=""0"">
                                    <span class=""qty-up"">+</span>
                                    <span class=""qty-down"">-</span>
                                </div>
                            </div>
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4e5c0045a87e6b53ba331f47188c5688852a5c2e17811", async() => {
                    WriteLiteral("<i class=\"fa fa-shopping-cart\"></i> send proposal");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                    <ul class=""product-btns"">
                        <li><a href=""#""><i class=""fa fa-heart-o""></i> add to wishlist</a></li>
                    </ul>

                    <ul class=""product-links"">
                        <li>Category:</li>
                        <li><a href=""#"">");
#nullable restore
#line 101 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                                   Write(Model.Category.CategoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a></li>
                    </ul>

                    <ul class=""product-links"">
                        <li>Share:</li>
                        <li><a href=""#""><i class=""fa fa-facebook""></i></a></li>
                        <li><a href=""#""><i class=""fa fa-twitter""></i></a></li>
                        <li><a href=""#""><i class=""fa fa-google-plus""></i></a></li>
                        <li><a href=""#""><i class=""fa fa-envelope""></i></a></li>
                    </ul>

                </div>
            </div>
            <!-- /Product details -->
            <!-- Product tab -->
            <div class=""col-md-12"">
                <div id=""product-tab"">
                    <!-- product tab nav -->
                    <ul class=""tab-nav"">
                        <li class=""active""><a data-toggle=""tab"" href=""#tab1"">Description</a></li>
                        <li><a data-toggle=""tab"" href=""#tab2"">Details</a></li>
                        <li><a data-toggle=""tab"" href=""#tab3"">Reviews (3)</a></");
            WriteLiteral(@"li>
                    </ul>
                    <!-- /product tab nav -->
                    <!-- product tab content -->
                    <div class=""tab-content"">
                        <!-- tab1  -->
                        <div id=""tab1"" class=""tab-pane fade in active"">
                            <div class=""row"">
                                <div class=""col-md-12"">
                                    <p>");
#nullable restore
#line 131 "D:\VS\Rent\Rent\Views\Home\Details.cshtml"
                                  Write(Model.ProductDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                </div>
                            </div>
                        </div>
                        <!-- /tab1  -->
                        <!-- tab2  -->
                        <div id=""tab2"" class=""tab-pane fade in"">
                            <div class=""row"">
                                <div class=""col-md-12"">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                                </div>
                            </div>
                        </div>
                        <!-- /tab2  -->
                   ");
            WriteLiteral(@"     <!-- tab3  -->
                        <div id=""tab3"" class=""tab-pane fade in"">
                            <div class=""row"">
                                <!-- Rating -->
                                <div class=""col-md-3"">
                                    <div id=""rating"">
                                        <div class=""rating-avg"">
                                            <span>4.5</span>
                                            <div class=""rating-stars"">
                                                <i class=""fa fa-star""></i>
                                                <i class=""fa fa-star""></i>
                                                <i class=""fa fa-star""></i>
                                                <i class=""fa fa-star""></i>
                                                <i class=""fa fa-star-o""></i>
                                            </div>
                                        </div>
                                        <ul clas");
            WriteLiteral(@"s=""rating"">
                                            <li>
                                                <div class=""rating-stars"">
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                </div>
                                                <div class=""rating-progress"">
                                                    <div style=""width: 80%;""></div>
                                                </div>
                                                <span class=""sum"">3</span>
                                            </li>
                                            <li>
                                ");
            WriteLiteral(@"                <div class=""rating-stars"">
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star-o""></i>
                                                </div>
                                                <div class=""rating-progress"">
                                                    <div style=""width: 60%;""></div>
                                                </div>
                                                <span class=""sum"">2</span>
                                            </li>
                                            <li>
                                                <div class=""rating-stars"">
                                                 ");
            WriteLiteral(@"   <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star-o""></i>
                                                    <i class=""fa fa-star-o""></i>
                                                </div>
                                                <div class=""rating-progress"">
                                                    <div></div>
                                                </div>
                                                <span class=""sum"">0</span>
                                            </li>
                                            <li>
                                                <div class=""rating-stars"">
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star""></i>
");
            WriteLiteral(@"                                                    <i class=""fa fa-star-o""></i>
                                                    <i class=""fa fa-star-o""></i>
                                                    <i class=""fa fa-star-o""></i>
                                                </div>
                                                <div class=""rating-progress"">
                                                    <div></div>
                                                </div>
                                                <span class=""sum"">0</span>
                                            </li>
                                            <li>
                                                <div class=""rating-stars"">
                                                    <i class=""fa fa-star""></i>
                                                    <i class=""fa fa-star-o""></i>
                                                    <i class=""fa fa-star-o""></i>
                         ");
            WriteLiteral(@"                           <i class=""fa fa-star-o""></i>
                                                    <i class=""fa fa-star-o""></i>
                                                </div>
                                                <div class=""rating-progress"">
                                                    <div></div>
                                                </div>
                                                <span class=""sum"">0</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <!-- /Rating -->
                                <!-- Reviews -->
                                <div class=""col-md-6"">
                                    <div id=""reviews"">
                                        <ul class=""reviews"">
                                            <li>
                                               ");
            WriteLiteral(@" <div class=""review-heading"">
                                                    <h5 class=""name"">John</h5>
                                                    <p class=""date"">27 DEC 2018, 8:0 PM</p>
                                                    <div class=""review-rating"">
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star-o empty""></i>
                                                    </div>
                                                </div>
                                                <div class=""review-body"">
                                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do ");
            WriteLiteral(@"eiusmod tempor incididunt ut labore et dolore magna aliqua</p>
                                                </div>
                                            </li>
                                            <li>
                                                <div class=""review-heading"">
                                                    <h5 class=""name"">John</h5>
                                                    <p class=""date"">27 DEC 2018, 8:0 PM</p>
                                                    <div class=""review-rating"">
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star-o empty""></i>
                                           ");
            WriteLiteral(@"         </div>
                                                </div>
                                                <div class=""review-body"">
                                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua</p>
                                                </div>
                                            </li>
                                            <li>
                                                <div class=""review-heading"">
                                                    <h5 class=""name"">John</h5>
                                                    <p class=""date"">27 DEC 2018, 8:0 PM</p>
                                                    <div class=""review-rating"">
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                   ");
            WriteLiteral(@"                     <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star""></i>
                                                        <i class=""fa fa-star-o empty""></i>
                                                    </div>
                                                </div>
                                                <div class=""review-body"">
                                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua</p>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <!-- /Reviews -->
                                <!-- Review Form -->
                                <div class=""col-md-3"">
                         ");
            WriteLiteral("           <div id=\"review-form\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4e5c0045a87e6b53ba331f47188c5688852a5c2e34812", async() => {
                WriteLiteral(@"
                                            <input class=""input"" type=""text"" placeholder=""Your Name"">
                                            <input class=""input"" type=""email"" placeholder=""Your Email"">
                                            <textarea class=""input"" placeholder=""Your Review""></textarea>
                                            <div class=""input-rating"">
                                                <span>Your Rating: </span>
                                                <div class=""stars"">
                                                    <input id=""star5"" name=""rating"" value=""5"" type=""radio""><label for=""star5""></label>
                                                    <input id=""star4"" name=""rating"" value=""4"" type=""radio""><label for=""star4""></label>
                                                    <input id=""star3"" name=""rating"" value=""3"" type=""radio""><label for=""star3""></label>
                                                    <input id=""star2"" name=""rating");
                WriteLiteral(@""" value=""2"" type=""radio""><label for=""star2""></label>
                                                    <input id=""star1"" name=""rating"" value=""1"" type=""radio""><label for=""star1""></label>
                                                </div>
                                            </div>
                                            <button class=""primary-btn"">Submit</button>
                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                    </div>
                                </div>
                                <!-- /Review Form -->
                            </div>
                        </div>
                        <!-- /tab3  -->
                    </div>
                    <!-- /product tab content  -->
                </div>
            </div>
            <!-- /product tab -->
        </div>
        <!-- /row -->
    </div>
    <!-- /container -->
</div>
<!-- /SECTION -->

<script>
    document.getElementsByClassName(""qty-up"")[0].addEventListener(""click"", function () {
        var val = parseInt(document.getElementById('proposedPrice').value)+1;
        document.getElementById('proposedPrice').value = val;
    });

        document.getElementsByClassName(""qty-down"")[0].addEventListener(""click"", function () {
            var val = parseInt(document.getElementById('proposedPrice').value);
            if (val!=0)
                document.getElementById('proposedPrice'");
            WriteLiteral(").value =val-1 ;\r\n        });\r\n</script>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
