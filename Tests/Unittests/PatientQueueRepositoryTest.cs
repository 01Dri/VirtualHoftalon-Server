using Moq;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Tests.MocksUtils;
using Xunit;

namespace VirtualHoftalon_Server.Tests.Unittests;

public class PatientQueueRepositoryTest
{

    private Mock<IPatientQueuesContextModels> mockContext;
    private IPatientsQueuesRepository _queuesRepository;

    public PatientQueueRepositoryTest()
    {
        this.mockContext = new Mock<IPatientQueuesContextModels>();
        this._queuesRepository = new PatientsQueuesRepository(mockContext.Object);
    }

    [Fact]
    public void TestCallPatientFromQueueBySectorIdWithPreferential()
    {
        Appointment mockAppointment = MockEntities.GetAppointmentMock();
        PatientsQueue mockPq1 = new PatientsQueue();
        PatientsQueue mockPq2 = new PatientsQueue();
        PatientsQueue mockPq3 = new PatientsQueue();

        mockPq1.Password = "X1";
        mockPq1.IsPreferred = true;
        mockPq1.Position = 1;
        mockPq1.Appointment = mockAppointment;

        mockPq2.Password = "X2";
        mockPq2.IsPreferred = false;
        mockPq2.Position = 2;
        mockPq2.Appointment = mockAppointment;

        mockPq3.Password = "X3";
        mockPq3.IsPreferred = true;
        mockPq3.Position = 3;
        mockPq3.Appointment = mockAppointment;

        IEnumerable<PatientsQueue?> mocksPatients = new List<PatientsQueue?>() { mockPq1, mockPq2, mockPq3 }; // Change to PatientsQueue?
        this.mockContext.Setup(r =>
            r.GetAllPatientsBySectorAndAppointmentHour(1, null))
            .Returns(mocksPatients);
        
        var result = this._queuesRepository.CallPatientOnQueueBySectorId(1);
        Assert.NotNull(result);
        Assert.Equal("X1", result.Password);
    }
    [Fact]
    public void TestCallPatientFromQueueBySectorIdWithoutPreferential()
    {
        Appointment mockAppointment = MockEntities.GetAppointmentMock();
        PatientsQueue mockPq1 = new PatientsQueue();
        PatientsQueue mockPq2 = new PatientsQueue();
        
        mockPq1.Password = "X5";
        mockPq1.IsPreferred = false;
        mockPq1.Position = 5;
        mockPq1.Appointment = mockAppointment;
        
        mockPq2.Password = "X4";
        mockPq2.IsPreferred = false;
        mockPq2.Position = 4;
        mockPq2.Appointment = mockAppointment;
        

        IEnumerable<PatientsQueue?> mocksPatients = new List<PatientsQueue?>() { mockPq1, mockPq2}; // Change to PatientsQueue?
        this.mockContext.Setup(r =>
                r.GetAllPatientsBySectorAndAppointmentHour(1, null))
            .Returns(mocksPatients);
        
        var result = this._queuesRepository.CallPatientOnQueueBySectorId(1);
        Assert.NotNull(result);
        Assert.Equal("X4", result.Password);
    }
    
    [Fact]
    public void TestEmptyListPatientQueuesException()
    {

        this.mockContext.Setup(r =>
                r.GetAllPatientsBySectorAndAppointmentHour(1, null))
            .Returns(new List<PatientsQueue>()); // Lista vazia
        
        Assert.Throws<EmptyListPatientQueuesException>(() => this._queuesRepository.CallPatientOnQueueBySectorId(1));
    }
}