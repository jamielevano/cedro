using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio
{
    public class Acceso
    {
        public bool IsAdmin(string usuario)
        {
            using (SMECEntities contexto = new SMECEntities())
            {
                return contexto.Usuario
                    .AsNoTracking()
                    .FirstOrDefault(x => x.login == usuario).admin;
            }
        }

        public object Permisos(string usuario, int paginaid)
        {
            using (SMECEntities contexto = new SMECEntities())
            {
                int rolid = contexto.Usuario
                    .AsNoTracking()
                    .FirstOrDefault(x => x.login == usuario).rolid;


                var item = contexto.Permiso.FirstOrDefault(y => y.rolid == rolid && y.paginaid == paginaid);

                if (item == null)
                {
                    return new
                    {
                        acceder = false,
                        agregar = false,
                        modificar = false,
                        eliminar = false
                    };
                }
                else
                {
                    return new
                    {
                        acceder = item.acceder,
                        agregar = item.agregar,
                        modificar = item.modificar,
                        eliminar = item.eliminar
                    };
                }
            }
        }

        public bool Login(string usuario, string clave, out bool admin, out bool coordinador,
            out int ejeid, out int telecentroid)
        {
            using (SMECEntities contexto = new SMECEntities())
            {
                try
                {
                    admin = contexto.Usuario
                        .Where(x => x.login == usuario && x.clave == clave)
                        .Select(x => x.admin)
                        .Single();

                    coordinador = contexto.Usuario
                        .Where(x => x.login == usuario && x.clave == clave)
                        .Select(x => x.coordinador?? false)
                        .Single();

                    telecentroid = contexto.Usuario
                        .Where(x => x.login == usuario && x.clave == clave)
                        .Select(x => x.telecentro??0)
                        .Single();

                    int Telecentroid = telecentroid;

                    ejeid = contexto.ListaDetalle
                        //.Where(x => x.listaid==33 && x.activo==1 && x.codigo== Telecentroid)
                        .Where(x => x.listaid == 54 && x.activo == 1 && x.codigo == Telecentroid)
                        .Select(x => x.relacionid ?? 0)
                        .Single();


                    return true;
                }
                catch (Exception ex)
                {
                    admin = false;
                    coordinador = false;
                    telecentroid = 0;
                    ejeid = 0;
                    return false;
                }
            }
        }

        public List<ModuloResponse> GetAcceso(string usuario)
        {
            using (var contexto = new SMECEntities())
            {
                var rolid = contexto.Usuario.FirstOrDefault(x => x.login == usuario).rolid;

                var admin = contexto.Usuario
                        .Where(x => x.login == usuario)
                        .Select(x => x.admin)
                        .Single();

                var menu = contexto.Modulo
                    .Where(mod => mod.SubModulo.Any(sub => sub.Pagina.Any(pag => admin || pag.Permiso.Any(a => a.rolid == rolid && a.acceder))) && !mod.aud_anulado )
                    .OrderBy(mod => mod.orden)
                    .Select(mod => new ModuloResponse
                    {
                        id = mod.id,
                        manual = mod.manual,
                        orden = mod.orden,
                        estado = mod.estado,
                        descripcion = mod.descripcion,
                        SubModuloResponse = mod.SubModulo
                            .Where(sub => sub.Pagina.Any(pag => admin || pag.Permiso.Any(a => a.rolid == rolid && a.acceder)) && !sub.aud_anulado)
                            .OrderBy(sub => sub.orden)
                            .Select(sub => new SubModuloResponse
                            {
                                id = sub.id,
                                descripcion = sub.descripcion,
                                orden = sub.orden,
                                estado = sub.estado,
                                PaginaResponse = sub.Pagina
                                    .Where(pag => (admin || pag.Permiso.Any(a => a.rolid == rolid && a.acceder)) && !pag.aud_anulado)
                                    .OrderBy(pag => pag.orden)
                                    .Select(pag => new PaginaResponse
                                    {
                                        id = pag.id,
                                        descripcion = pag.descripcion,
                                        img = pag.img,
                                        main = pag.main,
                                        orden = pag.orden,
                                        url = pag.url,
                                        estado = pag.estado,
                                    })
                            })
                    })
                    .ToList();

                return menu;
            }
        }
    }
}
