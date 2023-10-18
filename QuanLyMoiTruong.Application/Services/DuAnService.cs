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
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;

namespace QuanLyMoiTruong.Application.Services
{

    public class DuAnService : IDuAnService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGiayPhepMoiTruongService _giayPhepMoiTruongService;
        public DuAnService(IUnitOfWork unitOfWork, IGiayPhepMoiTruongService giayPhepMoiTruongService)
        {
            _unitOfWork= unitOfWork;
            _giayPhepMoiTruongService = giayPhepMoiTruongService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<DuAn>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<DuAnViewModel>>> GetAll()
        {
            var result = new List<DuAnViewModel>();
            var entities =  await _unitOfWork.GetRepository<DuAn>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<DuAnViewModel>>() { Data = result };
        }

        public async Task<ApiResult<DuAnViewModel>> GetById(int id)
        {
            var result = new DuAnViewModel();
            var data = await _unitOfWork.GetRepository<DuAn>().GetFirstOrDefaultAsync(predicate: x => x.IdDuAn == id, include: x => x.Include(y => y.KhuCongNghiep).Include(x=> x.DsGiayPhepMoiTruong));
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<DuAnViewModel>() {Data = result };
        }

        public async Task<ApiResult<DuAn>> Insert(DuAnViewModel obj)
        {
            var entity = new DuAn();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<DuAn>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<DuAn>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<DuAn>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<DuAn>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<DuAnViewModel>>> GetAllPaging(DuAnRequest request)
        {
            Expression<Func<DuAn, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenDoanhNghiep.ToLower().Contains(fullTextSearch)
                                      || p.TenDuAn.ToLower().Contains(fullTextSearch)
                                      || p.GiayPhepDKKD.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<DuAn>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<DuAnViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<DuAnViewModel>>() { Data = result };
        }

        public async Task<ApiResult<DuAn>> Update(DuAnViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<DuAn>().GetFirstOrDefaultAsync(predicate: x => x.IdDuAn == obj.IdDuAn);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<DuAn>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<DuAn>() { Data = entity };

        }
        public DuAnViewModel MapEntityToViewModel(DuAn entity) {
            var result = new DuAnViewModel();
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.TenDuAn;
            result.TenDoanhNghiep = entity.TenDoanhNghiep;
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep == null? 0: entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.KhuCongNghiep == null? "": entity.KhuCongNghiep.TenKhuCongNghiep;
            result.ThuocKhuKinhTe = entity.ThuocKhuKinhTe;
            result.DiaChi = entity.DiaChi;
            result.TenNguoiDaiDien = entity.TenNguoiDaiDien;
            result.TenNguoiPhuTrachTNMT = entity.TenNguoiPhuTrachTNMT;
            result.GiayPhepDKKD = entity.GiayPhepDKKD;
            result.QuyMo = entity.QuyMo;
            result.LoaiHinhSanXuat = entity.LoaiHinhSanXuat;
            result.GhiChu = entity.GhiChu;
            var listGPMT = _giayPhepMoiTruongService.GetListGiayPhepMoiTruongByDuAn(entity.IdDuAn).Result;
            result.DSGiayPhepMoiTruong = listGPMT.Data.ToList();
            return result;
        }
        public DuAn MapViewModelToEntity(DuAnViewModel viewModel, DuAn entity)
        {
            entity.IdDuAn = viewModel.IdDuAn;
            entity.TenDuAn = viewModel.TenDuAn;
            entity.TenDoanhNghiep = viewModel.TenDoanhNghiep;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep == 0 ? null: viewModel.IdKhuCongNghiep;
            entity.ThuocKhuKinhTe = viewModel.ThuocKhuKinhTe;
            entity.DiaChi = viewModel.DiaChi;
            entity.TenNguoiDaiDien = viewModel.TenNguoiDaiDien;
            entity.TenNguoiPhuTrachTNMT = viewModel.TenNguoiPhuTrachTNMT;
            entity.GiayPhepDKKD = viewModel.GiayPhepDKKD;
            entity.QuyMo = viewModel.QuyMo;
            entity.LoaiHinhSanXuat = viewModel.LoaiHinhSanXuat;
            entity.GhiChu = viewModel.GhiChu;
            return entity;
        }

        public async Task<ApiResult<bool>> InsertExcel(List<DuAnViewModel> listVm)
        {
            foreach(var obj in listVm)
            {

                var kcn = await _unitOfWork.GetRepository<KhuCongNghiep>().GetFirstOrDefaultAsync(predicate: x => obj.TenKhuCongNghiep.ToLower().Contains(x.TenKhuCongNghiep.ToLower()));
                obj.IdKhuCongNghiep = kcn != null? kcn.IdKhuCongNghiep: null;
                obj.ThuocKhuKinhTe = true;
                var entity = await Insert(obj);

                await _unitOfWork.SaveChangesAsync();
                var gpmtVm = obj.DSGiayPhepMoiTruong;
                foreach(var item in gpmtVm)
                {
                    item.IdDuAn = entity.Data.IdDuAn;
                    await _giayPhepMoiTruongService.Insert(item);
                }
            }
            return new ApiSuccessResult<bool>() { Data = true};
        }
        public async Task<ApiResult<List<string>>> ImportDuAn(IList<IFormFile> files)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            var ok = 0;
            List<DuAnViewModel> importData = new List<DuAnViewModel>();
            var messageSuccess = new List<string>();
            var messageError = new List<string>();
            var messageException = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                files[0].CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var ws2023 = currentSheet[0];
                    for (var i = 6; i <= ws2023.Dimension.End.Row; i++)
                    {
                        var imp = new DuAnViewModel();
                        var gpmt = new GiayPhepMoiTruongViewModel();
                        imp.TenDuAn = ws2023.Cells[i, 2].Value == null ? "" : ws2023.Cells[i, 2].Value.ToString();
                        imp.TenDoanhNghiep = ws2023.Cells[i, 3].Value == null ? "" : ws2023.Cells[i, 3].Value.ToString();
                        imp.DiaChi = ws2023.Cells[i, 4].Value == null ? "" : ws2023.Cells[i, 4].Value.ToString();
                        imp.TenKhuCongNghiep = ws2023.Cells[i, 5].Value == null ? "" : ws2023.Cells[i, 5].Value.ToString();
                        imp.TenNguoiDaiDien = ws2023.Cells[i, 6].Value == null ? "" : ws2023.Cells[i, 6].Value.ToString();
                        imp.LoaiHinhSanXuat = ws2023.Cells[i, 7].Value == null ? "" : ws2023.Cells[i, 7].Value.ToString();
                        imp.QuyMo = ws2023.Cells[i, 8].Value == null ? "" : ws2023.Cells[i, 8].Value.ToString();
                        gpmt.SoGiayPhep = ws2023.Cells[i, 9].Value == null ? "" : ws2023.Cells[i, 9].Value.ToString();
                        gpmt.NgayCap = ws2023.Cells[i, 10].Value == null ? "" : ws2023.Cells[i, 10].Value.ToString();
                        imp.DSGiayPhepMoiTruong.Add(gpmt);
                        if (!String.IsNullOrEmpty(imp.TenDuAn))
                        {
                            importData.Add(imp);
                        }
                    }
                    await InsertExcel(importData);
                }
            }
            result.Add(ok + " Doanh nghiệp được nhập hoàn chỉnh");
            result.Add("========== DANH SÁCH THÀNH CÔNG ==========");
            result.AddRange(messageSuccess);
            result.Add("========== DANH SÁCH LỖI ==========");
            result.AddRange(messageError);
            result.Add("========== DANH SÁCH EXCEPTION ==========");
            result.AddRange(messageException);
            return new ApiSuccessResult<List<string>>() { Data = result };
        }
    }

}
