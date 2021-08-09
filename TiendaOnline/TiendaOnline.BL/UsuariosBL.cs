using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.BL
{
    public class UsuariosBL
    {
        Contexto _contexto;
        public List<Usuario> ListadeUsuarios { get; set; }

        public UsuariosBL()
        {
            _contexto = new Contexto();
            ListadeUsuarios = new List<Usuario>();
        }

        public List<Usuario> ObtenerUsuarios()
        {
            ListadeUsuarios = _contexto.Usuarios
                .OrderBy(r => r.Nombre)
                .ToList();

            return ListadeUsuarios;
        }

        public List<Usuario> ObtenerUsuariosActivos()
        {
            ListadeUsuarios = _contexto.Usuarios
                .Where(r => r.Activo == true)
                .OrderBy(r => r.Nombre)
                .ToList();

            return ListadeUsuarios;
        }

        public void GuardarUsuario(Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                _contexto.Usuarios.Add(usuario);
            }
            else
            {
                var contrasenaEncriptada = Encriptar.CodificarContrasena(usuario.Contrasena);
                var usuarioExistente = _contexto.Usuarios.Find(usuario.Id);
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Contrasena = contrasenaEncriptada;
                usuarioExistente.Telefono= usuario.Telefono;
                usuarioExistente.Direccion = usuario.Direccion ;
                usuarioExistente.Activo = usuario.Activo;
            }
            _contexto.SaveChanges();
        }

        //public static class Encriptar
        //{
        //    public static string CodificarContrasena(string contrasena)
        //    {
        //        var salt = "MADs";

        //        byte[] bIn = Encoding.Unicode.GetBytes(contrasena);
        //        byte[] bSalt = Convert.FromBase64String(salt);
        //        byte[] bAll = new byte[bSalt.Length + bIn.Length];

        //        Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
        //        Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
        //        HashAlgorithm s = HashAlgorithm.Create("SHA512");
        //        byte[] bRet = s.ComputeHash(bAll);

        //        return Convert.ToBase64String(bRet);
        //    }
        //}

        public Usuario ObtenerUsuario(int id)
        {
            var usuario = _contexto.Usuarios.Find(id);
            return usuario;
        }
        

        public void EliminarUsuario(int id)
        {
            var usuario = _contexto.Usuarios.Find(id);

            _contexto.Usuarios.Remove(usuario);
            _contexto.SaveChanges();
        }
    }
}
