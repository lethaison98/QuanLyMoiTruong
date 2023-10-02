using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Globalization;
using System.Reflection.Metadata;
using QuanLyMoiTruong.Application.Requests;
using System.Linq.Expressions;
using QuanLyMoiTruong.Common.Expressions;
using QuanLyMoiTruong.Common.Enums;
using QuanLyMoiTruong.Application.Request;
using OfficeOpenXml;

namespace QuanLyMoiTruong.Application.Services
{

    public class ThanhPhanMoiTruongService : IThanhPhanMoiTruongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IKetQuaQuanTracService _ketQuaQuanTracService;

        public ThanhPhanMoiTruongService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IKetQuaQuanTracService ketQuaQuanTracService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
            _ketQuaQuanTracService = ketQuaQuanTracService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                var listKetQua = await _unitOfWork.GetRepository<KetQuaQuanTrac>().GetAllAsync(predicate: x => x.IdThanhPhanMoiTruong == id);
                foreach (var ketQua in listKetQua)
                {
                    await _ketQuaQuanTracService.Delete(ketQua.IdKetQuaQuanTrac);
                }
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IList<ThanhPhanMoiTruongViewModel>>> GetAll()
        {
            var result = new List<ThanhPhanMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).OrderByDescending(x=> x.Nam).ThenByDescending(x=> x.Lan).ToList();
            return new ApiSuccessResult<IList<ThanhPhanMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThanhPhanMoiTruongViewModel>> GetById(int id)
        {
            var result = new ThanhPhanMoiTruongViewModel();
            var data = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdThanhPhanMoiTruong == id);
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdThanhPhanMoiTruong, NhomTaiLieuEnum.ThanhPhanMoiTruong.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<ThanhPhanMoiTruongViewModel>() { Data = result };
        }

        public async Task<ApiResult<ThanhPhanMoiTruong>> Insert(ThanhPhanMoiTruongViewModel obj)
        {
            var entity = new ThanhPhanMoiTruong();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdThanhPhanMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.ThanhPhanMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            var listFile = await _fileTaiLieuService.GetByTaiLieu(idTaiLieu: entity.IdThanhPhanMoiTruong, nhomTaiLieu: NhomTaiLieuEnum.ThanhPhanMoiTruong.ToString());
            var fileExcel = listFile.Data.FirstOrDefault(x => x.LoaiFileTaiLieu == LoaiFileTaiLieuEnum.TongHopKetQuaQuanTrac.ToString());
            if(fileExcel != null)
            {
                await ImportKetQuaQuanTrac(fileExcel.File.LinkFile, entity);
            }
            return new ApiSuccessResult<ThanhPhanMoiTruong>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<ThanhPhanMoiTruong>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<ThanhPhanMoiTruongViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<ThanhPhanMoiTruong, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenThanhPhanMoiTruong.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<ThanhPhanMoiTruongViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<ThanhPhanMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThanhPhanMoiTruong>> Update(ThanhPhanMoiTruongViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdThanhPhanMoiTruong == obj.IdThanhPhanMoiTruong);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<ThanhPhanMoiTruong>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdThanhPhanMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.ThanhPhanMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<ThanhPhanMoiTruong>() { Data = entity };
        }
        public async Task<ApiResult<List<string>>> ImportKetQuaQuanTrac(string path, ThanhPhanMoiTruong tpmt)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            var ok = 0;
            var listKetQua = new List<KetQuaQuanTracViewModel>();
            var messageSuccess = new List<string>();
            var messageError = new List<string>();
            var messageException = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(path)))
            {
                var currentSheet = package.Workbook.Worksheets;

                var listKetQuaKhongKhi = new List<KetQuaQuanTracViewModel>();
                var wsKhongKhi = currentSheet[0];
                for (var i = 4; i <= wsKhongKhi.Dimension.End.Row; i++)
                { //9 điểm quan trắc không khí
                    for(var j = 5; j < 14; j++)
                    {
                        var imp = new KetQuaQuanTracViewModel();
                        imp.ChiTieu = wsKhongKhi.Cells[i, 2].Value == null ? "" : wsKhongKhi.Cells[i, 2].Value.ToString();
                        imp.DonViTinh = wsKhongKhi.Cells[i, 3].Value == null ? "" : wsKhongKhi.Cells[i, 3].Value.ToString();
                        imp.GiaTri = wsKhongKhi.Cells[i, j].Value == null ? "" : wsKhongKhi.Cells[i, j].Value.ToString();
                        imp.Nam = tpmt.Nam;
                        imp.Lan = tpmt.Lan;
                        imp.IdThanhPhanMoiTruong = tpmt.IdThanhPhanMoiTruong;   
                        imp.TenDiemQuanTrac = wsKhongKhi.Cells[3, j].Value == null ? "" : wsKhongKhi.Cells[3, j].Value.ToString();
                        listKetQuaKhongKhi.Add(imp);
                    }
                }
                await _ketQuaQuanTracService.InsertFromExcel(listKetQuaKhongKhi);

                var listKetQuaNuocMat = new List<KetQuaQuanTracViewModel>();
                var wsNuocMat = currentSheet[1];
                for (var i = 4; i <= wsNuocMat.Dimension.End.Row; i++)
                { //7 điểm quan trắc nước mặt
                    for (var j = 5; j < 12; j++)
                    {
                        var imp = new KetQuaQuanTracViewModel();
                        imp.ChiTieu = wsNuocMat.Cells[i, 2].Value == null ? "" : wsNuocMat.Cells[i, 2].Value.ToString();
                        imp.DonViTinh = wsNuocMat.Cells[i, 3].Value == null ? "" : wsNuocMat.Cells[i, 3].Value.ToString();
                        imp.GiaTri = wsNuocMat.Cells[i, j].Value == null ? "" : wsNuocMat.Cells[i, j].Value.ToString();
                        imp.Nam = tpmt.Nam;
                        imp.Lan = tpmt.Lan;
                        imp.IdThanhPhanMoiTruong = tpmt.IdThanhPhanMoiTruong;
                        imp.TenDiemQuanTrac = wsNuocMat.Cells[3, j].Value == null ? "" : wsNuocMat.Cells[3, j].Value.ToString();
                        listKetQuaNuocMat.Add(imp);
                    }
                }
                await _ketQuaQuanTracService.InsertFromExcel(listKetQuaNuocMat);

                var listKetQuaNuocNgam = new List<KetQuaQuanTracViewModel>();
                var wsNuocNgam = currentSheet[2];
                for (var i = 4; i <= wsNuocNgam.Dimension.End.Row; i++)
                { //2 điểm quan trắc nước ngầm
                    for (var j = 5; j < 7; j++)
                    {
                        var imp = new KetQuaQuanTracViewModel();
                        imp.ChiTieu = wsNuocNgam.Cells[i, 2].Value == null ? "" : wsNuocNgam.Cells[i, 2].Value.ToString();
                        imp.DonViTinh = wsNuocNgam.Cells[i, 3].Value == null ? "" : wsNuocNgam.Cells[i, 3].Value.ToString();
                        imp.GiaTri = wsNuocNgam.Cells[i, j].Value == null ? "" : wsNuocNgam.Cells[i, j].Value.ToString();
                        imp.Nam = tpmt.Nam;
                        imp.Lan = tpmt.Lan;
                        imp.IdThanhPhanMoiTruong = tpmt.IdThanhPhanMoiTruong;
                        imp.TenDiemQuanTrac = wsNuocNgam.Cells[3, j].Value == null ? "" : wsNuocNgam.Cells[3, j].Value.ToString();
                        listKetQuaNuocNgam.Add(imp);
                    }
                }
                await _ketQuaQuanTracService.InsertFromExcel(listKetQuaNuocNgam);

                var listKetQuaNuocBien = new List<KetQuaQuanTracViewModel>();
                var wsNuocBien = currentSheet[3];
                for (var i = 4; i <= wsNuocBien.Dimension.End.Row; i++)
                { //3 điểm quan trắc nước biển
                    for (var j = 5; j < 8; j++)
                    {
                        var imp = new KetQuaQuanTracViewModel();
                        imp.ChiTieu = wsNuocBien.Cells[i, 2].Value == null ? "" : wsNuocBien.Cells[i, 2].Value.ToString();
                        imp.DonViTinh = wsNuocBien.Cells[i, 3].Value == null ? "" : wsNuocBien.Cells[i, 3].Value.ToString();
                        imp.GiaTri = wsNuocBien.Cells[i, j].Value == null ? "" : wsNuocBien.Cells[i, j].Value.ToString();
                        imp.Nam = tpmt.Nam;
                        imp.Lan = tpmt.Lan;
                        imp.IdThanhPhanMoiTruong = tpmt.IdThanhPhanMoiTruong;
                        imp.TenDiemQuanTrac = wsNuocBien.Cells[3, j].Value == null ? "" : wsNuocBien.Cells[3, j].Value.ToString();
                        listKetQuaNuocBien.Add(imp);
                    }
                }
                await _ketQuaQuanTracService.InsertFromExcel(listKetQuaNuocBien);

                return new ApiSuccessResult<List<string>>() { Data = result };
            }
        }

        public ThanhPhanMoiTruongViewModel MapEntityToViewModel(ThanhPhanMoiTruong entity)
        {
            var result = new ThanhPhanMoiTruongViewModel();
            result.IdThanhPhanMoiTruong = entity.IdThanhPhanMoiTruong;
            result.TenThanhPhanMoiTruong = entity.TenThanhPhanMoiTruong;
            result.GhiChu = entity.GhiChu;
            result.Nam = entity.Nam;
            result.Lan = entity.Lan;
            return result;
        }
        public ThanhPhanMoiTruong MapViewModelToEntity(ThanhPhanMoiTruongViewModel viewModel, ThanhPhanMoiTruong entity)
        {
            entity.IdThanhPhanMoiTruong = viewModel.IdThanhPhanMoiTruong;
            entity.TenThanhPhanMoiTruong = viewModel.TenThanhPhanMoiTruong;
            entity.GhiChu = viewModel.GhiChu;
            entity.Nam = viewModel.Nam;
            entity.Lan = viewModel.Lan;
            return entity;
        }
    }

}
