﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

      <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
        <title>Emobank</title>
        
        <!-- Google Fonts -->
        <link href='https://fonts.googleapis.com/css?family=Roboto:400,500,700,300' rel='stylesheet' type='text/css'>
        <link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,300,700' rel='stylesheet' type='text/css'>
        
        <!-- Font Awesome -->
        <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">
        
        <!-- pretty-photo -->
        <link href="css/prettyPhoto.css" rel="stylesheet">

        <!-- Bootstrap -->
        <link href="css/bootstrap.min.css" rel="stylesheet">
        
        <!-- slick carousel -->
        <link href="css/slick.css" rel="stylesheet">
        
        <!-- animate -->
        <link href="css/animate.css" rel="stylesheet">
        
        <!-- animate -->
        <link href="css/datepicker.css" rel="stylesheet">
        
        <!-- Main Style -->
        <link href="style.css" rel="stylesheet">
        <link href="responsive.css" rel="stylesheet">

        <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="wrapper" class="homepage-1"> <!-- Wrapper -->
            
            <div id="header" class="wow fadeInDown"><!-- Header -->
                <div class="container">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="logo-wrap">
                                <a href="index.aspx" class="logo">
                                    <img src="images/logo-header.png" alt="logo" class="img-responsive main_logo">
									<!--<img src="images/Logo4.png"  class="mobile_logo" style="display:none"/>-->
                                </a>
                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="info-nav-wrap"> <!-- Top section -->
                                <div class="info-section">
                                    <div class="right">
                                        <div class="help">
                                            <a href="">
                                                <i class="fa fa-mail"></i>
                                                <div class="left">
                                                    <span>Email</span>Pharmalab@gmail.com
                                                </div>
                                            </a>
                                        </div>
                                        <div class="call">
                                            <a href="tel:+985-524-223">
                                                <i class="fa fa-mobile"></i>
                                                <div class="left">
                                                    <span>Chiamiaci</span>00378/0549 905826
                                                </div>
                                                <div class="clearfix"></div>
                                            </a>
                                        </div>
                                        <div class="top-booking">
                                            <a href="login.aspx" class="btn btn-default btn-booking">ACCESSO</a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div> <!-- Top section -->
                            
                            <div class="main-nav-wrap"> <!-- Main navigation -->
                                
                                <nav class="navbar navbar-default">
                                    <div class="container-fluid">
                                        <!-- Brand and toggle get grouped for better mobile display -->
                                        <div class="navbar-header">
                                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                                                <span class="sr-only">Toggle navigation</span>
                                                <span class="icon-bar"></span>
                                                <span class="icon-bar"></span>
                                                <span class="icon-bar"></span>
                                            </button>
                                        </div>
                                        <!-- Collect the nav links, forms, and other content for toggling -->
                                        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                                            <ul class="nav navbar-nav" style="float:right">
                                              <!--  
                                                -->
												
                                            
                                                <li><a href="#">CLINICHE VETERINARIE</a></li>
                                                <li><a href="#">Banca del Sangue ed Emoderivati</a></li>

                                                
                                             
                                                <li><a href="contact.html">CONTATTACI</a></li>
                                            </ul>
                                        </div><!-- /.navbar-collapse -->
                                    </div><!-- /.container-fluid -->
                                </nav>
                                
                            </div> <!-- Main navigation -->
                            
                        </div>
                    </div>
                </div>
            </div><!-- Header -->
            
            <div id="slider-section" class="wow fadeInUp"><!-- Slider -->
                <div id="home-slider">
                    <div class="item">
                        <img src="images/slide-2.jpg" alt="slide-2" class="img-responsive"/>
                       <!-- <div class="slider-desc">
                            <div class="container">
                                <div class="content">
                                    <div class="small-title-slider font-title">Get</div>
                                    <div class="big-title-slider font-title">Blood Bank <span>Subheading</span></div>
                                    <a href="" class="btn btn-default btn-booking">Dummy Text</a>
                                </div>
                            </div>
                        </div>-->
                    </div>
                    
                </div>
            </div><!-- Slider -->
            
            <div class="button-banner-wrap wow fadeInDown"><!-- Btn Banner -->
                <div class="container">
                    <div class="row button-banner">
                        <div class="col-md-4 first">
                            <a class="btn btn-default">
                                <div class="inner">
                                    <i class="fa fa-clock-o"></i><span>ORARI DI APERTURA</span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-4 mid">
                            <a class="btn btn-default">
                                <div class="inner">
                                    <i class="fa fa-medkit"></i><span>Emergenze</span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-4 last">
                            <a class="btn btn-default">
                                <div class="inner">
                                    <i class="fa fa-stethoscope"></i><span>MEDICI CALENDARIO</span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div><!-- Btn Banner -->
            
            <div id="services-section" class="wow fadeInDown"> <!-- Services Tab -->
                
                <div class="services-tab-nav-wrap"><!-- Tab  Nav-->
                    <div class="container">
                        <div class="text-center">
                            <ul class="nav nav-tabs tab-services" role="tablist">
                                <li role="presentation" class="active">
                                    <a href="#services" aria-controls="services" role="tab" data-toggle="tab">
                                       I NOSTRI SERVIZI
                                    </a>
                                </li>
                                <li role="presentation">
                                    <a href="#why" aria-controls="why" role="tab" data-toggle="tab">
                                La Nostra Missione
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div><!-- Tab  Nav-->
                
                <div class="services-tab-content-wrap" style="background:none repeat scroll 0 0 #eeeeeb;padding:20px;"><!-- Tab  Content -->
                    <div class="container">
                        <div class="tab-content">
                            
                            <div role="tabpanel" class="tab-pane fade in active" id="services"><!-- Tab  Content #1 -->
                                <div class="row">
                                    <div class="col-md-12">
                                      <p style="border-radius: 7px;  color: rgb(163, 51, 0); border: 2px solid rgb(192, 192, 192); font-size: 20px; font-weight: 500; padding: 20px; margin-bottom: 0px;">Eva nasce di pari passo allo sviluppo e alla regolamentazione della Medicina Trasfusionale Veterinaria.<br><br>
La nostra azienda offre prodotti CERTIFICATI, SICURI e soprattutto REPERIBILI H24, grazie ai seguenti servizi:<br>
-database dei donatori selezionati per la specie canina, felina ed equina<br>
-esami del sangue e test diagnostici effettuati su ogni prelievo<br>
-produzione e stoccaggio delle sacche attraverso una filiera dedicata<br>
-conservazione entro i termini stabiliti dalla gazzetta ufficiale<br>
-servizio di consegna in emergenza con mantenimento della catena del freddo.<br>
-cross match test compreso nell'acquisto della sacca, per testare la compatibilità tra<br>
donatore e ricevente direttamente dal medico curante<br><br>
Eva fará la differenza in tutti i casi clinici e chirurgici in cui la difficile reperibilità di sangue
intero condiziona fortemente la prognosi, perché grazie ai nostri servizi il medico curante
ha a disposizione un prodotto sicuro nel suo impiego, tracciabile e a disposizione anche in
situazioni di estrema emergenza.</p>
                                    </div>
                                  
                                 
                                </div>
                            </div><!-- Tab  Content #1 -->
                            
                            <div role="tabpanel" class="tab-pane fade" id="why"><!-- Tab  Content #2 -->
                                <div class="row">
                                    <div class="col-md-12">
                                       
                                            <div class="row">
                                              
                                                <div class="col-md-12">
                                                    <div class="services-excerpt">
                                                       
                                                        <p style="border-radius: 7px; text-align: center; font-style: italic; color: rgb(163, 51, 0); border: 2px solid rgb(192, 192, 192); font-size: 20px; font-weight: 500; padding: 41px; margin-bottom: 0px;">
                                                            AL VOSTRO FIANCO PER IL LORO BENESSERE
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                     
                                       
                                        
                                    </div>
                                    
                             
                             
                                    
                                </div>
                            </div><!-- Tab  Content #2 -->
                            
                        </div>
                    </div>
                </div><!-- Tab  Content -->
                
            </div><!-- Services Tab -->
            
            <div id="testimonial-section" class="wow fadeInDown"><!-- Testimonial -->
                <div class="hr">
                </div>
                <div class="testimonial-wrap" style="display:none">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-5" style="display:none">
                                <div id="testimonial-images">
                                    <div class="item">
                                        <img src="images/testimony-1.png" alt="" class="img-responsive">
                                    </div>
                                   
                                </div>
                            </div>
                            <div class="col-md-12">
                                <h3 class="font-title">Descrizione Generale</h3>
                                <div id="testimonial-desc">
                                    <div>
                                        <div class="testimonial-content" style="display:NONE">
                                            <p class="font-title">
                                                </p>
                                            <div class="name font-title">Richard Maxmarge<span>Creative Director</span></div>
                                            
                                        </div>
                                    </div>
                                    
                                  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div><!-- Testimonial -->
            

    
            <div class="footer-credits wow fadeInUp"><!-- Footer Copy right/credits -->
                <div class="container">
                    <div class="credits">Copyright Administration’s Center Florence Italy</div>
                    <div class="ft-soc">
                        <a href=""><i class="fa fa-facebook"></i></a>
                        <a href=""><i class="fa fa-twitter"></i></a>
                        <a href="" class="last"><i class="fa fa-google-plus"></i></a>
                    </div>
                </div>
            </div><!-- Footer Copy right/credits -->
            
            
        </div> <!-- Wrapper -->
        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
        <!-- Include all compiled plugins (below), or include individual files as needed -->
        <script src="js/bootstrap.min.js"></script>
        <!-- slick carousel -->
        <script src="js/slick.js"></script>
        <!-- waypoint -->
        <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
        <!-- pretty photo -->
        <script src="js/jquery.prettyPhoto.js"></script>
        <!-- quicksand -->
        <script src="js/quicksand.js"></script>
        <!-- quicksand -->
        <script src="js/wow.js"></script>
        <!-- Countup -->
        <script src="js/jquery.counterup.min.js"></script>
        <!-- Countup -->
        <script src="js/datepicker.js"></script>
        <!-- theme script -->
        <script src="js/theme-script.js"></script>
    </div>
    </form>
</body>
</html>
