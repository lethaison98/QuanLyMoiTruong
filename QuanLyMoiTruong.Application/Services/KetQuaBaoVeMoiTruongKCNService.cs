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

namespace QuanLyMoiTruong.Application.Services
{

    public class KetQuaBaoVeMoiTruongKCNService : IKetQuaBaoVeMoiTruongKCNService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IDiemQuanTracService _diemQuanTracService;

        public KetQuaBaoVeMoiTruongKCNService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IDiemQuanTracService diemQuanTracService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
            _diemQuanTracService = diemQuanTracService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IList<KetQuaBaoVeMoiTruongKCNViewModel>>> GetAll()
        {
            var result = new List<KetQuaBaoVeMoiTruongKCNViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<KetQuaBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaBaoVeMoiTruongKCNViewModel>> GetById(int id)
        {
            var result = new KetQuaBaoVeMoiTruongKCNViewModel();
            var data = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaBaoVeMoiTruongKCN == id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<KetQuaBaoVeMoiTruongKCNViewModel>() { Data = result };
        }

        public async Task<ApiResult<KetQuaBaoVeMoiTruongKCN>> Insert(KetQuaBaoVeMoiTruongKCNViewModel obj)
        {
            var entity = new KetQuaBaoVeMoiTruongKCN();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaBaoVeMoiTruongKCN>() { Data = entity };
        }
        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<KetQuaBaoVeMoiTruongKCNViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<KetQuaBaoVeMoiTruongKCN, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenKhuCongNghiep.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<KetQuaBaoVeMoiTruongKCNViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<KetQuaBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaBaoVeMoiTruongKCN>> Update(KetQuaBaoVeMoiTruongKCNViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaBaoVeMoiTruongKCN == obj.IdKetQuaBaoVeMoiTruongKCN);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaBaoVeMoiTruongKCN>() { Data = entity };
        }
        public async Task<ApiResult<bool>> InsertFromExcel(List<KetQuaBaoVeMoiTruongKCNViewModel> list)
        {
            try
            {
                foreach (var obj in list)
                {
                    var entity = new KetQuaBaoVeMoiTruongKCN();
                    entity = MapViewModelToEntity(obj, entity);
                    await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().InsertAsync(entity);
                }
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<bool>() { Data = false };
            }
            return new ApiSuccessResult<bool>() { Data = true };
        }
        public async Task<ApiResult<List<KetQuaBaoVeMoiTruongKCNViewModel>>> GetAllByIdBaoCaoBaoVeMoiTruong(int IdBaoCaoBaoVeMoiTruong)
        {
            var result = new List<KetQuaBaoVeMoiTruongKCNViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().GetAllAsync(predicate: x => x.IdBaoCaoBaoVeMoiTruong == IdBaoCaoBaoVeMoiTruong && !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<List<KetQuaBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }
        public async Task<ApiResult<List<KetQuaBaoVeMoiTruongKCNViewModel>>> GetBaoCao1_2(BaoCaoBaoVeMoiTruongRequest request)
        {
            var result = new List<KetQuaBaoVeMoiTruongKCNViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongKCN>().GetAllAsync(predicate: x => !x.IsDeleted, include: x => x.Include(x => x.BaoCaoBaoVeMoiTruong));
            if (request.TuNgay != null)
            {
                entities = entities.Where(x => x.BaoCaoBaoVeMoiTruong.NgayBaoCao >= request.TuNgay).ToList();
            }
            if (request.DenNgay != null)
            {
                entities = entities.Where(x => x.BaoCaoBaoVeMoiTruong.NgayBaoCao <= request.DenNgay).ToList();
            }
            if(request.Nam != 0)
            {
                entities = entities.Where(x => x.BaoCaoBaoVeMoiTruong.Nam == request.Nam).ToList();
            }
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<List<KetQuaBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }
        public KetQuaBaoVeMoiTruongKCNViewModel MapEntityToViewModel(KetQuaBaoVeMoiTruongKCN entity)
        {
            var result = new KetQuaBaoVeMoiTruongKCNViewModel();
            result.IdKetQuaBaoVeMoiTruongKCN = entity.IdKetQuaBaoVeMoiTruongKCN;
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.TenKhuCongNghiep;
            result.DiaChi = entity.DiaChi;
            result.DienTich = entity.DienTich;
            result.TenChuDautu = entity.TenChuDautu;
            result.SoLuongCoSo = entity.SoLuongCoSo.ToString();
            result.TyLeLapDay = entity.TyLeLapDay;
            result.HeThongThuGomNuocMua = entity.HeThongThuGomNuocMua? "Có": "Không";
            result.TongLuongNuocThai = entity.TongLuongNuocThai;
            result.CongSuatThietKeHTXLNT = entity.CongSuatThietKeHTXLNT;
            result.HeThongQuanTracNuocThai = entity.HeThongQuanTracNuocThai;
            result.ChatThaiRanSinhHoat = entity.ChatThaiRanSinhHoat;
            result.ChatThaiRanCongNghiep = entity.ChatThaiRanCongNghiep;
            result.ChatThaiRanNguyHai = entity.ChatThaiRanNguyHai;
            result.CongTrinhPhongNgua = entity.CongTrinhPhongNgua;
            result.TyLeCayXanh = entity.TyLeCayXanh;
            return result;
        }
        public KetQuaBaoVeMoiTruongKCN MapViewModelToEntity(KetQuaBaoVeMoiTruongKCNViewModel viewModel, KetQuaBaoVeMoiTruongKCN entity)
        {
            entity.IdKetQuaBaoVeMoiTruongKCN = viewModel.IdKetQuaBaoVeMoiTruongKCN;
            entity.IdBaoCaoBaoVeMoiTruong = viewModel.IdBaoCaoBaoVeMoiTruong;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep;
            entity.TenKhuCongNghiep = viewModel.TenKhuCongNghiep;
            entity.DiaChi = viewModel.DiaChi;
            entity.DienTich = viewModel.DienTich;
            entity.TenChuDautu = viewModel.TenChuDautu;
            entity.SoLuongCoSo = Int32.Parse(viewModel.SoLuongCoSo);
            entity.TyLeLapDay = viewModel.TyLeLapDay;
            entity.HeThongThuGomNuocMua  = viewModel.HeThongThuGomNuocMua == "Có" ? true : false;
            entity.TongLuongNuocThai = viewModel.TongLuongNuocThai;
            entity.CongSuatThietKeHTXLNT = viewModel.CongSuatThietKeHTXLNT;
            entity.HeThongQuanTracNuocThai = viewModel.HeThongQuanTracNuocThai;
            entity.ChatThaiRanSinhHoat = viewModel.ChatThaiRanSinhHoat;
            entity.ChatThaiRanCongNghiep = viewModel.ChatThaiRanCongNghiep;
            entity.ChatThaiRanNguyHai = viewModel.ChatThaiRanNguyHai;
            entity.CongTrinhPhongNgua = viewModel.CongTrinhPhongNgua;
            entity.TyLeCayXanh = viewModel.TyLeCayXanh;
            return entity;
        }
    }

}
