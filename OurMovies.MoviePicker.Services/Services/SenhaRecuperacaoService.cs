using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Enum;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Services.Notification;
using OurMovies.MoviePicker.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Services.Services
{
	public class SenhaRecuperacaoService
	{
		public void RecuperSenha(DTOContato contato, INotification notification, TipoContato tipoContato = TipoContato.EMAIL)
		{
			AutenticacaoService autenticacaoService = new AutenticacaoService();

			var usuarioCtx = autenticacaoService.GetUsuario(contato.Usuario);

			if (usuarioCtx != null)
			{
				autenticacaoService.ResetarSenhaUsuario(new DTOUsuario { Usuario = contato.Usuario }, out string novaSenha);

				notification.Notificar(contato, $"Olá {contato.Nome}, aqui sua senha temporária.", MontaHTML(new SenhaAcesso { Usuario = contato.Usuario, Senha = novaSenha }, contato.Nome));
			}

			else
				throw new Exception("Usuário não encontrado na base.");

		}
		public void RecuperarSenhaGerarToken(DTOContato contato, INotification notification, string urlCaminho)
		{
			AutenticacaoService autenticacaoService = new AutenticacaoService();

			var usuarioCtx = autenticacaoService.GetUsuario(contato.Usuario);

			if (usuarioCtx != null)
			{
				notification.Notificar(contato, $"Olá {contato.Nome}, o link para resetar sua senha chegou!", EmailRecuperacao(usuarioCtx, contato.Nome, urlCaminho));
			}
			else
				throw new Exception("Usuário não encontrado na base.");
		}
        public bool RecuperarSenhaValidarToken(string token, out SenhaAcesso usuario)
        {
			usuario = null;

            if (!string.IsNullOrEmpty(token))
            {
				token = token.Replace(" ", "+");
				string tokenDecrypted = Criptografia.Decrypt(token);

				var arrValoresToken = tokenDecrypted.Split(';');

				var userId = Int32.Parse(arrValoresToken[0]);
				var validadeToken = DateTime.Parse(arrValoresToken[1]);

				if(validadeToken > DateTime.Now && userId != 0)
                {
					AutenticacaoService autenticacaoService = new AutenticacaoService();

					var usuarioCtx = autenticacaoService.GetUsuario(userId);
					usuario = usuarioCtx;

					return true;
                }

				return false;
            }

			return false;
        }

        private string EmailRecuperacao(SenhaAcesso usuario, string nome, string url)
		{
			string token = GerarToken(usuario);
			 
			string html = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional //EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
				<html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'>
				<head>
				<!--[if gte mso 9]><xml><o:OfficeDocumentSettings><o:AllowPNG/><o:PixelsPerInch>96</o:PixelsPerInch></o:OfficeDocumentSettings></xml><![endif]-->
				<meta content='text/html; charset=utf-8' http-equiv='Content-Type'/>
				<meta content='width=device-width' name='viewport'/>
				<!--[if !mso]><!-->
				<meta content='IE=edge' http-equiv='X-UA-Compatible'/>
				<!--<![endif]-->
				<title></title>
				<!--[if !mso]><!-->
				<!--<![endif]-->
				<style type='text/css'>
						body {
							margin: 0;
							padding: 0;
						}

						table,
						td,
						tr {
							vertical - align: top;
							border-collapse: collapse;
						}

						* {
							line - height: inherit;
						}

						a[x-apple-data-detectors=true] {
							color: inherit !important;
							text-decoration: none !important;
						}
					</style>
				<style id='media-query' type='text/css'>
						@media (max-width: 670px) {

							.block - grid,
							.col {
								min-width: 320px !important;
								max-width: 100% !important;
								display: block !important;
							}

							.block-grid {
								width: 100% !important;
							}

							.col {
								width: 100% !important;
							}

							.col>div {
								margin: 0 auto;
							}

							img.fullwidth,
							img.fullwidthOnMobile {
								max-width: 100% !important;
							}

							.no-stack .col {
								min-width: 0 !important;
								display: table-cell !important;
							}

							.no-stack.two-up .col {
								width: 50% !important;
							}

							.no-stack .col.num4 {
								width: 33% !important;
							}

							.no-stack .col.num8 {
								width: 66% !important;
							}

							.no-stack .col.num4 {
								width: 33% !important;
							}

							.no-stack .col.num3 {
								width: 25% !important;
							}

							.no-stack .col.num6 {
								width: 50% !important;
							}

							.no-stack .col.num9 {
								width: 75% !important;
							}

							.video-block {
								max-width: none !important;
							}

							.mobile_hide {
								min-height: 0px;
								max-height: 0px;
								max-width: 0px;
								display: none;
								overflow: hidden;
								font-size: 0px;
							}

							.desktop_hide {
								display: block !important;
								max-height: none !important;
							}
						}
					</style>
				</head>
				<body class='clean-body' style='margin: 0; padding: 0; -webkit-text-size-adjust: 100%; background-color: #3d1554;'>
				<!--[if IE]><div class='ie-browser'><![endif]-->
				<table bgcolor='#3d1554' cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='table-layout: fixed; vertical-align: top; min-width: 320px; Margin: 0 auto; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #3d1554; width: 100%;' valign='top' width='100%'>
				<tbody>
				<tr style='vertical-align: top;' valign='top'>
				<td style='word-break: break-word; vertical-align: top;' valign='top'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td align='center' style='background-color:#3d1554'><![endif]-->
				<div style='background-color:#57366e;'>
				<div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 650px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;'>
				<div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#57366e;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:650px'><tr class='layout-full-width' style='background-color:transparent'><![endif]-->
				<!--[if (mso)|(IE)]><td align='center' width='650' style='background-color:transparent;width:650px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;'><![endif]-->
				<div class='col num12' style='min-width: 320px; max-width: 650px; display: table-cell; vertical-align: top; width: 650px;'>
				<div style='width:100% !important;'>
				<!--[if (!mso)&(!IE)]><!-->
				<div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'>
				<!--<![endif]-->
				<table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'>
				<tbody>
				<tr style='vertical-align: top;' valign='top'>
				<td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;' valign='top'>
				<table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='0' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 0px; width: 100%;' valign='top' width='100%'>
				<tbody>
				<tr style='vertical-align: top;' valign='top'>
				<td height='0' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td>
				</tr>
				</tbody>
				</table>
				</td>
				</tr>
				</tbody>
				</table>
				<!--[if (!mso)&(!IE)]><!-->
				</div>
				<!--<![endif]-->
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
				</div>
				</div>
				</div>
				<div style='background-color:transparent;'>
				<div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 650px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;'>
				<div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:650px'><tr class='layout-full-width' style='background-color:transparent'><![endif]-->
				<!--[if (mso)|(IE)]><td align='center' width='650' style='background-color:transparent;width:650px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:35px; padding-bottom:0px;'><![endif]-->
				<div class='col num12' style='min-width: 320px; max-width: 650px; display: table-cell; vertical-align: top; width: 650px;'>
				<div style='width:100% !important;'>
				<!--[if (!mso)&(!IE)]><!-->
				<div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:35px; padding-bottom:0px; padding-right: 0px; padding-left: 0px;'>
				<!--<![endif]-->
				<!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 10px; padding-left: 10px; padding-top: 10px; padding-bottom: 10px; font-family: Arial, sans-serif'><![endif]-->
				<div style='color:#ffffff;font-family:Poppins, Arial, Helvetica, sans-serif;line-height:1.5;padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;'>
				<div style='line-height: 1.5; font-size: 12px; color: #ffffff; font-family: Poppins, Arial, Helvetica, sans-serif; mso-line-height-alt: 18px;'>
				<p style='font-size: 18px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 27px; margin: 0;'><span style='font-size: 18px;'>Olá, " + nome +
				$@"!</span></p>
				<p style='font-size: 18px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 27px; margin: 0;'><span style='font-size: 18px;'>Aqui está o link para atualizar sua senha.</span></p>
				<p style='font-size: 14px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 21px; margin: 0;'> </p>
				<p style='font-size: 14px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 21px; margin: 0;'><em><span style='font-size: 11px;'>É importante que a senha seja atualizada.</span></em></p>
				</div>
				</div>
				<!--[if mso]></td></tr></table><![endif]-->
				<!--[if (!mso)&(!IE)]><!-->
				</div>
				<!--<![endif]-->
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
				</div>
				</div>
				</div>
				<div style='background-color:transparent;'>
				<div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 650px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #57366e;'>
				<div style='border-collapse: collapse;display: table;width: 100%;background-color:#57366e;'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:650px'><tr class='layout-full-width' style='background-color:#57366e'><![endif]-->
				<!--[if (mso)|(IE)]><td align='center' width='650' style='background-color:#57366e;width:650px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 30px; padding-left: 30px; padding-top:55px; padding-bottom:55px;'><![endif]-->
				<div class='col num12' style='min-width: 320px; max-width: 650px; display: table-cell; vertical-align: top; width: 650px;'>
				<div style='width:100% !important;'>
				<!--[if (!mso)&(!IE)]><!-->
				<div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:55px; padding-bottom:55px; padding-right: 30px; padding-left: 30px;'>
				<!--<![endif]-->
				<div align='center' class='button-container' style='padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;'>
				<!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;'><tr><td style='padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px' align='center'><v:roundrect xmlns:v='urn:schemas-microsoft-com:vml' xmlns:w='urn:schemas-microsoft-com:office:word' href='{url}?token={token}' style='height:44.25pt; width:177.75pt; v-text-anchor:middle;' arcsize='51%' strokeweight='1.5pt' strokecolor='#795E8B' fillcolor='#3d1554'><w:anchorlock/><v:textbox inset='0,0,0,0'><center style='color:#ffffff; font-family:Arial, sans-serif; font-size:12px'><![endif]-->"+$@"<a href='{url}?token={token}' style='-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #3d1554; border-radius: 30px; -webkit-border-radius: 30px; -moz-border-radius: 30px; width: auto; width: auto; border-top: 2px solid #795E8B; border-right: 2px solid #795E8B; border-bottom: 2px solid #795E8B; border-left: 2px solid #795E8B; padding-top: 18px; padding-bottom: 18px; font-family: Poppins, Arial, Helvetica, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;' target='_blank'><span style='padding-left:35px;padding-right:35px;font-size:12px;display:inline-block;'><span style='line-height: 14px; word-break: break-word;'>Mudar senha</span></span></a>
				<!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->
				</div>
				<!--[if (!mso)&(!IE)]><!-->
				</div>
				<!--<![endif]-->
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
				</div>
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				</td>
				</tr>
				</tbody>
				</table>
				<!--[if (IE)]></div><![endif]-->
				</body>
				</html>";

			return html;

		}

		private string GerarToken(SenhaAcesso usuario)
		{
			var tokenBase = $"{usuario.Id};{DateTime.Now.AddMinutes(30).ToString()}";

			var token = Criptografia.Encrypt(tokenBase);

			return token;
		}
		private string MontaHTML(SenhaAcesso usuario, string nome)
		{
			string html = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional //EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
				<html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'>
				<head>
				<!--[if gte mso 9]><xml><o:OfficeDocumentSettings><o:AllowPNG/><o:PixelsPerInch>96</o:PixelsPerInch></o:OfficeDocumentSettings></xml><![endif]-->
				<meta content='text/html; charset=utf-8' http-equiv='Content-Type'/>
				<meta content='width=device-width' name='viewport'/>
				<!--[if !mso]><!-->
				<meta content='IE=edge' http-equiv='X-UA-Compatible'/>
				<!--<![endif]-->
				<title></title>
				<!--[if !mso]><!-->
				<!--<![endif]-->
				<style type='text/css'>
						body {
							margin: 0;
							padding: 0;
						}

						table,
						td,
						tr {
							vertical - align: top;
							border-collapse: collapse;
						}

						* {
							line - height: inherit;
						}

						a[x-apple-data-detectors=true] {
							color: inherit !important;
							text-decoration: none !important;
						}
					</style>
				<style id='media-query' type='text/css'>
						@media (max-width: 670px) {

							.block - grid,
							.col {
								min-width: 320px !important;
								max-width: 100% !important;
								display: block !important;
							}

							.block-grid {
								width: 100% !important;
							}

							.col {
								width: 100% !important;
							}

							.col>div {
								margin: 0 auto;
							}

							img.fullwidth,
							img.fullwidthOnMobile {
								max-width: 100% !important;
							}

							.no-stack .col {
								min-width: 0 !important;
								display: table-cell !important;
							}

							.no-stack.two-up .col {
								width: 50% !important;
							}

							.no-stack .col.num4 {
								width: 33% !important;
							}

							.no-stack .col.num8 {
								width: 66% !important;
							}

							.no-stack .col.num4 {
								width: 33% !important;
							}

							.no-stack .col.num3 {
								width: 25% !important;
							}

							.no-stack .col.num6 {
								width: 50% !important;
							}

							.no-stack .col.num9 {
								width: 75% !important;
							}

							.video-block {
								max-width: none !important;
							}

							.mobile_hide {
								min-height: 0px;
								max-height: 0px;
								max-width: 0px;
								display: none;
								overflow: hidden;
								font-size: 0px;
							}

							.desktop_hide {
								display: block !important;
								max-height: none !important;
							}
						}
					</style>
				</head>
				<body class='clean-body' style='margin: 0; padding: 0; -webkit-text-size-adjust: 100%; background-color: #3d1554;'>
				<!--[if IE]><div class='ie-browser'><![endif]-->
				<table bgcolor='#3d1554' cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='table-layout: fixed; vertical-align: top; min-width: 320px; Margin: 0 auto; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #3d1554; width: 100%;' valign='top' width='100%'>
				<tbody>
				<tr style='vertical-align: top;' valign='top'>
				<td style='word-break: break-word; vertical-align: top;' valign='top'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td align='center' style='background-color:#3d1554'><![endif]-->
				<div style='background-color:#57366e;'>
				<div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 650px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;'>
				<div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#57366e;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:650px'><tr class='layout-full-width' style='background-color:transparent'><![endif]-->
				<!--[if (mso)|(IE)]><td align='center' width='650' style='background-color:transparent;width:650px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;'><![endif]-->
				<div class='col num12' style='min-width: 320px; max-width: 650px; display: table-cell; vertical-align: top; width: 650px;'>
				<div style='width:100% !important;'>
				<!--[if (!mso)&(!IE)]><!-->
				<div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'>
				<!--<![endif]-->
				<table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'>
				<tbody>
				<tr style='vertical-align: top;' valign='top'>
				<td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;' valign='top'>
				<table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='0' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 0px; width: 100%;' valign='top' width='100%'>
				<tbody>
				<tr style='vertical-align: top;' valign='top'>
				<td height='0' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td>
				</tr>
				</tbody>
				</table>
				</td>
				</tr>
				</tbody>
				</table>
				<!--[if (!mso)&(!IE)]><!-->
				</div>
				<!--<![endif]-->
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
				</div>
				</div>
				</div>
				<div style='background-color:transparent;'>
				<div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 650px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;'>
				<div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:650px'><tr class='layout-full-width' style='background-color:transparent'><![endif]-->
				<!--[if (mso)|(IE)]><td align='center' width='650' style='background-color:transparent;width:650px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:35px; padding-bottom:0px;'><![endif]-->
				<div class='col num12' style='min-width: 320px; max-width: 650px; display: table-cell; vertical-align: top; width: 650px;'>
				<div style='width:100% !important;'>
				<!--[if (!mso)&(!IE)]><!-->
				<div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:35px; padding-bottom:0px; padding-right: 0px; padding-left: 0px;'>
				<!--<![endif]-->
				<!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 10px; padding-left: 10px; padding-top: 10px; padding-bottom: 10px; font-family: Arial, sans-serif'><![endif]-->
				<div style='color:#ffffff;font-family:Poppins, Arial, Helvetica, sans-serif;line-height:1.5;padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;'>
				<div style='line-height: 1.5; font-size: 12px; color: #ffffff; font-family: Poppins, Arial, Helvetica, sans-serif; mso-line-height-alt: 18px;'>
				<p style='font-size: 18px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 27px; margin: 0;'><span style='font-size: 18px;'>Olá, " + nome + @"!</span></p>
				<p style='font-size: 18px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 27px; margin: 0;'><span style='font-size: 18px;'>Sua nova senha temporária é: " + usuario.Senha + @"</span></p>
				<p style='font-size: 14px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 21px; margin: 0;'> </p>
				<p style='font-size: 14px; line-height: 1.5; word-break: break-word; text-align: center; mso-line-height-alt: 21px; margin: 0;'><em><span style='font-size: 11px;'>É importante que a senha seja atualizada.</span></em></p>
				</div>
				</div>
				<!--[if mso]></td></tr></table><![endif]-->
				<!--[if (!mso)&(!IE)]><!-->
				</div>
				<!--<![endif]-->
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
				</div>
				</div>
				</div>
				<div style='background-color:transparent;'>
				<div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 650px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #57366e;'>
				<div style='border-collapse: collapse;display: table;width: 100%;background-color:#57366e;'>
				<!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:650px'><tr class='layout-full-width' style='background-color:#57366e'><![endif]-->
				<!--[if (mso)|(IE)]><td align='center' width='650' style='background-color:#57366e;width:650px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 30px; padding-left: 30px; padding-top:55px; padding-bottom:55px;'><![endif]-->
				<div class='col num12' style='min-width: 320px; max-width: 650px; display: table-cell; vertical-align: top; width: 650px;'>
				<div style='width:100% !important;'>
				<!--[if (!mso)&(!IE)]><!-->
				<div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:55px; padding-bottom:55px; padding-right: 30px; padding-left: 30px;'>
				<!--<![endif]-->
				<div align='center' class='button-container' style='padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;'>
				<!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;'><tr><td style='padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px' align='center'><v:roundrect xmlns:v='urn:schemas-microsoft-com:vml' xmlns:w='urn:schemas-microsoft-com:office:word' href='/Home/Senha' style='height:44.25pt; width:177.75pt; v-text-anchor:middle;' arcsize='51%' strokeweight='1.5pt' strokecolor='#795E8B' fillcolor='#3d1554'><w:anchorlock/><v:textbox inset='0,0,0,0'><center style='color:#ffffff; font-family:Arial, sans-serif; font-size:12px'><![endif]--><a href='/Home/Senha' style='-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #3d1554; border-radius: 30px; -webkit-border-radius: 30px; -moz-border-radius: 30px; width: auto; width: auto; border-top: 2px solid #795E8B; border-right: 2px solid #795E8B; border-bottom: 2px solid #795E8B; border-left: 2px solid #795E8B; padding-top: 18px; padding-bottom: 18px; font-family: Poppins, Arial, Helvetica, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;' target='_blank'><span style='padding-left:35px;padding-right:35px;font-size:12px;display:inline-block;'><span style='line-height: 14px; word-break: break-word;'>Mudar senha</span></span></a>
				<!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->
				</div>
				<!--[if (!mso)&(!IE)]><!-->
				</div>
				<!--<![endif]-->
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
				</div>
				</div>
				</div>
				<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
				</td>
				</tr>
				</tbody>
				</table>
				<!--[if (IE)]></div><![endif]-->
				</body>
				</html>";

			return html;
		}

	}
}
